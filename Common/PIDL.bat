cd %~dp0
"C:\Program Files (x86)\Nettention\ProudNet\util\PIDL.exe" -cs .\PIDL\S2C.PIDL -outdir .\PIDL
"C:\Program Files (x86)\Nettention\ProudNet\util\PIDL.exe" -cs .\PIDL\C2S.PIDL -outdir .\PIDL
copy .\PIDL\*.cs ..\Server\RMI\
copy .\PIDL\*.cs ..\Client\RMI\
pause