# TestLILAB.

## Configuracion de base de datos.

- Configurar en el archivo "appsettings.json" el campo de "DefaultConnection", insertando la cadena de conexion correspondiente a su equipo. 
- Ejecutan el commando el siguiente comando para inicializar la migracion:
    - dotnet ef migrations add InitialCreate.
- Ejecutan el commando el siguiente comando para cargar la migracion:
    - dotnet ef database update.

## Ejecutar Test

Para ejecutar el test, pude realizarse desde Visual Studio.
  - Depurar -> Iniciar sin depuracion.
