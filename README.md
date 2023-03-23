# TotalControl-EE
Proyect Total Control of Entry and Exit of the personal

------------------------------------------------------------ENGLISH----------------------------------------------------------------------
This REST API is an ASP.NET Core application in .NET 7, it has four services that are responsible for performing different tasks, it provides endpoints to access resources related to employees and employee sign-in and sign-out records.

It has the following services:

1. A CRUD (Create, Read, Update, Delete) service for employees.
2. A CRUD (Create, Read, Update, Delete) service for employee entry and exit records.
3. A service that lists the amount of entries and exits given a date (from - to), which can be filtered by name or surname and branch.
4. A service that returns the average number of men and women entering and leaving per month, for each existing branch given a date (from – to).

The prerequisites to execute the project are the following:

-Download Microsoft Visual Studio or another IDE
-For the API to work, you will need to install the following NuGet packages:
     *AutoMapper (version 12.0.1)
     *AutoMapper.Extensions.Microsoft.DependencyInjection (version 12.0.0)
     *Microsoft.AspNetCore.JsonPatch (version 7.0.0)
     *Microsoft.AspNetCore.Mvc.NewtonsoftJson (version 7.0.0)
     *Microsoft.AspNetCore.OpenApi (version 7.0.4)
     *Microsoft.EntityFrameworkCore.SqlServer (version 7.0.0)
     *Microsoft.EntityFrameworkCore.Tools (version 7.0.0)
     *Swashbuckle.AspNetCore (version 6.4.0)
-Download SQL SERVER MANAGEMENT



------------- START IMPORT REPOSITORY------------------

-To copy the GitHub repository to your computer, follow these steps:

     1.Open a terminal or command prompt on your computer.
     2.Navigate to the folder where you want to save the repository using the cd path/folder command.
     3.Once in the desired folder, use the command that is in quotes
     "git clone https://github.com/ang3lrdgz/TotalControl-EE.git" to clone the GitHub repository.

-Configure the connection string to the database. This can be done in the appsettings.json file found in the root of the project (it already comes with the settings for the repository script.)
         Example:

         "ConnectionStrings": {
             "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
         }





------------- END IMPORT REPOSITORY------------------





------------- START IMPORT DATABASE------------------

-To install a .sql file in SQL Server Management Studio (SSMS), you can follow these steps:
1.Login to the SQL Server using a user account that has administrator permissions.
2.Open SQL Server Management Studio (SSMS) and connect to the server.
3.In SSMS, right-click on the database you want to run the script on and select "New Query".
4.In the new query window, click on "File" on the menu bar and select "Open".
5.Navigate to the location of the .sql file you want to install and select the file.
6.Click "Open" to open the file in the SSMS query window.
7.Verify that the file was opened correctly and that the contents of the file are what you want to execute.
8.Click "Run" or press the F5 key to run the SQL script.

Once the SQL script is executed successfully, the database will have been modified according to the content of the script. You can verify that the tables, views, stored procedures, among others, have been created or updated correctly in the database.

-You can also use the following commands in the Package Manager Console to create a database directly to sql server from the project with 4 default records in the employees table.

-add-migration "MigrationName"
-update-database




-------------END IMPORT DATABASE------------------




------------------------------------------------------------ESPAÑOL----------------------------------------------------------------------



Esta API REST es una aplicación de ASP.NET Core en .NET 7, tiene cuatro servicios que se encargan de realizar diferentes tareas, proporciona endpoints para acceder a recursos relacionados con empleados y registros de ingresos y egresos de los empleados.

Cuenta con los siguientes servicios:

1. Un servicio CRUD (Create, Read, Update, Delete) de empleados.
2. Un servicio CRUD (Create, Read, Update, Delete) de registros de ingreso y egreso de empleados.
3. Un servicio que lista la cantidad de ingresos y egresos dada una fecha (desde – hasta), que se puede filtrar por nombre o apellido y sucursal.
4. Un servicio que devuelve el promedio de hombres y mujeres que ingresan y egresan por mes, por cada sucursal existente dada una fecha (desde – hasta).

los requisitos previos para ejecutar el proyecto son los siguientes:

-Descargar Microsoft Visual Studio u otro IDE
-Para que la API funcione, deberás instalar los siguientes paquetes NuGet:
    *AutoMapper (versión 12.0.1)
    *AutoMapper.Extensions.Microsoft.DependencyInjection (versión 12.0.0)
    *Microsoft.AspNetCore.JsonPatch (versión 7.0.0)
    *Microsoft.AspNetCore.Mvc.NewtonsoftJson (versión 7.0.0)
    *Microsoft.AspNetCore.OpenApi (versión 7.0.4)
    *Microsoft.EntityFrameworkCore.SqlServer (versión 7.0.0)
    *Microsoft.EntityFrameworkCore.Tools (versión 7.0.0)
    *Swashbuckle.AspNetCore (versión 6.4.0)
-Descargar SQL SERVER MANAGEMENT



------------- INICIO IMPORTAR REPOSITORIO------------------

-Para copiar el repositorio de GitHub a su computadora, siga los siguientes pasos:

    1.Abra una terminal o línea de comandos en su computadora.
    2.Navegue a la carpeta donde desea guardar el repositorio usando el comando cd ruta/carpeta.
    3.Una vez en la carpeta deseada, use el comando que esta entre comillas
    "git clone https://github.com/ang3lrdgz/TotalControl-EE.git" para clonar el repositorio de GitHub.

-Configurar la cadena de conexión a la base de datos. Esto se puede hacer en el archivo appsettings.json que se encuentra en la raíz del proyecto(ya viene con la configuracion para el script del repositorio.)
        Ejemplo:

        "ConnectionStrings": {
            "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
        }





------------- FIN IMPORTAR REPOSITORIO------------------





------------- INICIO IMPORTAR BASE DE DATOS------------------

-Para instalar un archivo .sql en SQL Server Management Studio (SSMS), puedes seguir estos pasos:
1.Inicia sesión en el servidor de SQL Server utilizando una cuenta de usuario que tenga permisos de administrador.
2.Abre SQL Server Management Studio (SSMS) y conecta al servidor.
3.En SSMS, haz clic con el botón derecho en la base de datos en la que deseas ejecutar el script y selecciona "Nueva consulta".
4.En la nueva ventana de consulta, haz clic en "Archivo" en la barra de menú y selecciona "Abrir".
5.Navega hasta la ubicación del archivo .sql que deseas instalar y selecciona el archivo.
6.Haz clic en "Abrir" para abrir el archivo en la ventana de consulta de SSMS.
7.Verifica que el archivo se abrió correctamente y que el contenido del archivo es lo que deseas ejecutar.
8.Haz clic en "Ejecutar" o presiona la tecla F5 para ejecutar el script SQL.

Una vez que se ejecuta correctamente el script SQL, la base de datos habrá sido modificada según el contenido del script. Puedes verificar que las tablas, vistas, procedimientos almacenados, entre otros, se hayan creado o actualizado correctamente en la base de datos.

-Tambien se puede usar en la Consola del administrador de paquetes los siguienes comandos para crear una base de datos directamente a sql server desde el proyecto con 4 registros predeterminados en la tabla empleados.

-add-migration "NombreDeMigration"
-update-database




-------------FIN IMPORTAR BASE DE DATOS------------------

