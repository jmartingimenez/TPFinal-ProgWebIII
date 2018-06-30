# Requisitos

  - Visual Studio 2015 o superior (No probado en versión 2012)
  - SQL Server 2012 o superior
  - Base de datos 'PW3TP_20181C_Tareas' creada

# Instrucciones

  - Ejecutar el script SQL 'PW3TP_20181C_Tareas__Datos.sql'
  - Generar el archivo 'connection.config'

# Configuración

 - Crear una copia del archivo 'connection.config.template' y nombrar esta 'connection.config'. Dentro de este nuevo archivo, configurar los datos necesarios para la conexión a la base de datos.

```xml
<connectionStrings>
  <add name="PW3TP_20181C_TareasEntities"
       connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;
       provider connection string=&quot;
       data source=_INSTANCIA_SQL_;
       initial catalog=PW3TP_20181C_Tareas;
       persist security info=True;
       user id=_USUARIO_SQL_;
       password=_PASSWORD_SQL_;
       MultipleActiveResultSets=True;
       App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
</connectionStrings>
```

> Los atributos importantes a editar son...

> 'data source' (Instancia de SQL. Ejemplo: 'SQLEXPRESS')

> 'user id' (Usuario de SQL. Ejemplo: 'Sa')

> 'password' (Password de SQL. Ejemplo: '')

# Grupo de trabajo

* [Marcos Betancor] 
* [Mauro Ledesma] 
* [Jonatan Gimenez] 

    [Marcos Betancor]: <https://github.com/marcosbetancor>
    [Mauro Ledesma]: <https://github.com/mau-ws>
    [Jonatan Gimenez]: <https://github.com/jmartingimenez>
