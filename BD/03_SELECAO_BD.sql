USE DB_Senatur;

SELECT * FROM TBL_Usuario;
SELECT* FROM TBL_TipoUsuario;
SELECT * FROM TBL_Pacote;

SELECT IdTipoUsuario, Email, TituloTipoUsuario
FROM TBL_TipoUsuario TI INNER JOIN TBL_Usuario U ON TI.IdTipoUsuario = U.FK_IdTipoUsuario 

SELECT IdPacote FROM TBL_Pacote
Where IdPacote = 1;