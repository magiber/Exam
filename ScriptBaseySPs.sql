EATE PROCEDURE InsertarEmpleado

@Fotografia VARCHAR(100),
@Nombre VARCHAR(100),
@Apellidos VARCHAR(100),
@PuestoId INT,
@FechaNacimineto DATE,
@Direccion VARCHAR(100),
@Telefono VARCHAR(100),
@CorreoElectronico VARCHAR(100),
@EstadoId INT

AS 
BEGIN 

  INSERT INTO Empleado 
  ( Fotografia, Nombre, Apellidos, PuestoId, FechaNacimineto, Direccion, Telefono, CorreoElectronico, EstadoId)
  VALUES
  (@Fotografia,@Nombre,@Apellidos,@PuestoId,@FechaNacimineto,@Direccion,@Telefono,@CorreoElectronico,@EstadoId)
  
END