; --- INSTALADOR DE VERTICE POS (Inno Setup 6) ---
;
; Antes de compilar: publicar el proyecto WinForms en Release (perfil FolderProfile).
; La versión NO se escribe acá: se lee del WinForms.exe publicado (<Version> en WinForms.csproj).
;
; El instalador es genérico: no lleva datos de ningún cliente ni certificados de ARCA.
;   - Configuración de cada PC:  C:\ProgramData\VerticeApp\appsettings.local.json
;   - Certificados y claves:      C:\ProgramData\VerticeApp\Certificados
; Ninguna de las dos carpetas se toca al actualizar.

#define MyAppName "Vertice POS"
#define MyAppPublisher "Vertice Proyectos SA"
#define MyAppExeName "WinForms.exe"
#define MyPublishFolder AddBackslash(SourcePath) + "..\Vertice\bin\Release\net8.0-windows\publish\win-x64"

#if !FileExists(MyPublishFolder + "\" + MyAppExeName)
  #error No se encontró el WinForms.exe publicado. Publicar el proyecto WinForms en Release antes de compilar el instalador.
#endif

; Traba de seguridad: si por error quedó alguna clave privada en la carpeta publish, no se
; compila. Con la clave se puede facturar en nombre del contribuyente.
#if FindFirst(MyPublishFolder + "\*.key", 0) || FindFirst(MyPublishFolder + "\Services\*.key", 0)
  #error Hay archivos .key en la carpeta publish. Las claves de ARCA no pueden ir en el instalador.
#endif

; Versión de 3 partes (ej. 1.9.0), igual a <Version> del WinForms.csproj.
#define VerMajor
#define VerMinor
#define VerRev
#define VerBuild
#expr GetVersionComponents(MyPublishFolder + "\" + MyAppExeName, VerMajor, VerMinor, VerRev, VerBuild)
#define MyAppVersion Str(VerMajor) + "." + Str(VerMinor) + "." + Str(VerRev)

[Setup]
AppId={{8A663528-9842-4D93-9602-53697960613D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=Output
OutputBaseFilename=Instalador_VerticePOS_{#MyAppVersion}
Compression=lzma
SolidCompression=yes
; Necesario para crear carpetas en ProgramData y Program Files
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64compatible
; Si quedara algún archivo del sistema en uso, Windows lo cierra antes de reemplazarlo.
; (La espera a que el sistema se cierre está en InitializeSetup, más abajo.)
CloseApplications=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Dirs]
; C:\ProgramData\VerticeApp con permisos totales para los usuarios: ahí viven la base (.mdf),
; la configuración de la PC y los certificados, y el sistema los escribe sin ser administrador.
Name: "{commonappdata}\VerticeApp"; Permissions: users-full
Name: "{commonappdata}\VerticeApp\Certificados"; Permissions: users-full

[Files]
Source: "{#MyPublishFolder}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
; Firewall para el servidor del celular (Api\VerticeApi.exe, puerto 5000). Se borra y se vuelve a
; crear para no acumular reglas repetidas en cada actualización. profile=any porque Windows suele
; marcar el wifi del local como red "Pública"; el acceso igual queda protegido por el PIN.
Filename: "{sys}\netsh.exe"; Parameters: "advfirewall firewall delete rule name=""{#MyAppName} - Servidor celular"""; Flags: runhidden
Filename: "{sys}\netsh.exe"; Parameters: "advfirewall firewall add rule name=""{#MyAppName} - Servidor celular"" dir=in action=allow program=""{app}\Api\VerticeApi.exe"" protocol=TCP localport=5000 profile=any"; Flags: runhidden
; Sin "skipifsilent": cuando la actualización se lanza desde el sistema (modo /SILENT, con
; la barra de progreso visible), al terminar vuelve a abrir el sistema solo.
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall

[UninstallRun]
Filename: "{sys}\netsh.exe"; Parameters: "advfirewall firewall delete rule name=""{#MyAppName} - Servidor celular"""; Flags: runhidden; RunOnceId: "BorrarReglaFirewall"

[Code]
const
  APP_MUTEX = 'VerticePOS_App'; // Lo crea el sistema mientras está abierto (Program.cs)

// Cuando la actualización se lanza desde el propio sistema, el instalador arranca mientras el
// sistema todavía se está cerrando. Se espera hasta 30 segundos a que termine; si no, en modo
// manual se le pide al usuario que lo cierre, y en modo automático se cancela sin tocar nada.
function InitializeSetup(): Boolean;
var
  Intentos: Integer;
begin
  Intentos := 0;
  while CheckForMutexes(APP_MUTEX) and (Intentos < 60) do
  begin
    Sleep(500);
    Intentos := Intentos + 1;
  end;

  while CheckForMutexes(APP_MUTEX) do
  begin
    if WizardSilent then
    begin
      Log('Vertice POS sigue abierto: se cancela la actualización.');
      Result := False;
      Exit;
    end;

    if MsgBox('Vertice POS está abierto. Ciérrelo y presione Aceptar para continuar.',
              mbError, MB_OKCANCEL) = IDCANCEL then
    begin
      Result := False;
      Exit;
    end;
  end;

  Result := True;
end;

// Migración única de las instalaciones viejas, que tenían los datos del cliente en
// {app}\appsettings.json y los certificados en {app}\Services. Corre ANTES de copiar los
// archivos nuevos (que traen un appsettings.json genérico). Nunca pisa nada que ya exista,
// así que en las actualizaciones siguientes no hace nada.
procedure MigrarConfiguracionYCertificados();
var
  DataDir, LocalCfg, OldCfg, CertDir, ServicesDir, Ext: String;
  FindRec: TFindRec;
begin
  DataDir := ExpandConstant('{commonappdata}\VerticeApp');
  LocalCfg := DataDir + '\appsettings.local.json';
  OldCfg := ExpandConstant('{app}\appsettings.json');
  CertDir := DataDir + '\Certificados';
  ServicesDir := ExpandConstant('{app}\Services');

  ForceDirectories(CertDir);

  if (not FileExists(LocalCfg)) and FileExists(OldCfg) then
  begin
    if CopyFile(OldCfg, LocalCfg, True) then
      Log('Configuración del cliente migrada a ' + LocalCfg)
    else
      Log('No se pudo migrar la configuración a ' + LocalCfg);
  end;

  if DirExists(ServicesDir) and FindFirst(ServicesDir + '\*', FindRec) then
  begin
    try
      repeat
        Ext := Lowercase(ExtractFileExt(FindRec.Name));
        if ((FindRec.Attributes and FILE_ATTRIBUTE_DIRECTORY) = 0) and ((Ext = '.crt') or (Ext = '.key')) then
        begin
          // True = no pisa si ya existe en Certificados.
          if CopyFile(ServicesDir + '\' + FindRec.Name, CertDir + '\' + FindRec.Name, True) then
            Log('Certificado migrado: ' + FindRec.Name);
        end;
      until not FindNext(FindRec);
    finally
      FindClose(FindRec);
    end;
  end;
end;

// El sistema cierra el servidor del celular al salir, pero si se cortó la luz o se cerró de golpe
// puede quedar corriendo solo y trabar los archivos de {app}\Api.
procedure CerrarServidorCelular();
var
  ResultCode: Integer;
begin
  Exec(ExpandConstant('{sys}\taskkill.exe'), '/F /IM VerticeApi.exe', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssInstall then
  begin
    CerrarServidorCelular();
    MigrarConfiguracionYCertificados();
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usUninstall then
    CerrarServidorCelular();
end;
