FOR /d /r . %%d IN ("Library") DO @IF EXIST "%%d" rd /s /q "%%d"

FOR /d /r . %%d IN ("obj") DO @IF EXIST "%%d" rd /s /q "%%d"

FOR /d /r . %%d IN ("UIElementsSchema") DO @IF EXIST "%%d" rd /s /q "%%d"

FOR /d /r . %%d IN ("Logs") DO @IF EXIST "%%d" rd /s /q "%%d"

FOR /d /r . %%d IN ("Packages") DO @IF EXIST "%%d" rd /s /q "%%d"

FOR /d /r . %%d IN ("Saved") DO @IF EXIST "%%d" rd /s /q "%%d"

del /S *.sln