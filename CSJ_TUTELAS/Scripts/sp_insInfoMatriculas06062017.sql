USE [COPNIA]
GO
/****** Object:  StoredProcedure [dbo].[sp_insInfoMatriculas]    Script Date: 6/06/2017 11:28:48 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_insInfoMatriculas]
   --Matriculas
   @mtmatricula CHAR(25),
   --Profesion por matricula, si una Persona tiene dos profesiones debe tener dos Matriculas
   @nombreProfesion VARCHAR(160),
   --Universidad por egresado, si es de dos universidades debe tener dos registros
   @nombreUniversidad VARCHAR(100),
   --Resolución seccional
   @mt_rsnumero INT,
   @mt_rsanio INT,
   @mt_rsseccional INT,
   --Resolucion Nacional el mismo numero emitido por el Otro Consejo
   @mt_rnnumero INT,
   @mt_rnfecha VARCHAR(10),
   @mt_rntipo INT,   
   --Otros
   @mtfechagrado VARCHAR(10), 
     --PERSONA
   @psnumident VARCHAR(15), --mtpersona INT,
   @pstipoident VARCHAR(3),
   @psprimernombre VARCHAR(50),
   @pssegundonombre VARCHAR(50),
   @psprimerapellido VARCHAR(50),
   @pssegundoapellido VARCHAR(50),
   @psfechanacimiento VARCHAR(10),
   @pssexo VARCHAR(1),
    --Campos de Auditoria y Control
   @idUsuario VARCHAR(50),
   @idHomologacion INT,
   --retorno del mensaje
   @err_message VARCHAR(4000) OUTPUT
AS
 DECLARE 
 @mtpersona INT,
 @mensaje INT,
 @idPersona INT,
 @idUniversidad INT,
 @idProfesion INT,
 @manumero INT--,
-- @matipo  INT
BEGIN
    SET @err_message = ''
	IF NOT EXISTS (Select [manumero]+1 from [dbo].[Matriculas] where [mamatricula] = @mtmatricula)  
	BEGIN TRY
		  --SET @mensaje= @mensaje + ' No existe la matricula' 
		  Select @manumero  = (Select Max([manumero]) + 1 from [dbo].[Matriculas] where [matipo]=  @mt_rntipo);
		  INSERT INTO [dbo].[Matriculas]
			   ([mamatricula]
			   ,[maestado]
			   ,[manumero]
			   ,[matipo]
			   ,[idUsuario]
			   ,[idHomologacion])
		 VALUES
			   (@mtmatricula
			   ,'A'
			   ,@manumero 
			   ,@mt_rntipo 			  
			   ,@idUsuario 
			   ,@idHomologacion);--idUsuario 
		--	SET    @err_message = 'Inserto la Matricula' + (SELECT CAST (@mtmatricula AS VARCHAR(25))); 
	END TRY  
	BEGIN CATCH  
			--SELECT ERROR_NUMBER() AS ErrorNumber ,ERROR_MESSAGE() AS ErrorMessage  
			SET @err_message = @err_message +' error al insertar matricula: ' + (SELECT CAST (@mtmatricula AS VARCHAR(25)))
	END CATCH;	 
	ELSE
		BEGIN
			SET @err_message = @err_message  +' verifique, por que es posible que esta matricula esta y no se puede recargar, número: ' + (SELECT CAST (@mtmatricula AS VARCHAR(25)))
		END
	IF NOT EXISTS (Select [psid]  from [dbo].[Personas] where [psnumident] = @psnumident) 
		BEGIN TRY
		 
		   INSERT INTO [dbo].[Personas]
			   ([pstipoident]
			   ,[psnumident]
			   ,[psprimernombre]
			   ,[pssegundonombre]
			   ,[psprimerapellido]
			   ,[pssegundoapellido]
			 --  ,[psfechanacimiento] 
			 --  ,[pssexo]    
			  -- ,[psnacionalidad]
			   ,[idUsuario]
			   )
		 VALUES
			   ('CC',--@pstipoident ,
				'B009',--@psnumident ,
				'AA',
				'AA' ,
				'AA',
				'AA', 
			--	@psfechanacimiento,
			--	@pssexo,
			--	'0022',--    ,<psnacionalidad, nvarchar(50),>
				'FREDYAST')
		  SET @mensaje= @mensaje + ' Inserto la persona '  + @psnumident 
		 -- INSERT INTO [dbo].[Personas]
			--   ([pstipoident]
			--   ,[psnumident]
			--   ,[psprimernombre]
			--   ,[pssegundonombre]
			--   ,[psprimerapellido]
			--   ,[pssegundoapellido]
			--   ,[psfechanacimiento] 
			--   ,[pssexo]    
			--   ,[psnacionalidad]
			--   ,[idUsuario]
			--   )
		 --VALUES
			--   (@pstipoident ,
			--	@mt_rsanio ,
			--	@psprimernombre ,
			--	@pssegundonombre ,
			--	@psprimerapellido,
			--	@pssegundoapellido, 
			--	@psfechanacimiento,
			--	@pssexo,
			--	'0022',--    ,<psnacionalidad, nvarchar(50),>
			--	@idUsuario)
		END TRY  
		BEGIN CATCH  
			SELECT   				ERROR_NUMBER() AS ErrorNumber  			   ,ERROR_MESSAGE() AS ErrorMessage;  
			SET @err_message = @err_message + ' No se pudo Insertar la persona para la matricula : ' +  (SELECT CAST (@mtmatricula AS VARCHAR(25))) ; 
		END CATCH ;
	--IF NOT EXISTS (Select [rnnumero], [rnano], [rntipo] from [dbo].[ResNacional] 
	--			   where [rnnumero] = @mt_rsnumero
	--			   and [rnano] = @mt_rsanio)
	--	 BEGIN TRY
	--	   INSERT INTO [dbo].[ResNacional]
	--		   ([rnnumero]
	--		   ,[rnano]
	--		   ,[rntipo]
	--		   ,[rnfecha]
	--		   ,[rnObservacion]
	--		  -- ,[rnCierra]          -- ,[rnnamemod]         --  ,[rnfechamod]
	--		   ,[idUsuario])
	--	 VALUES
	--		   (@mt_rsnumero
	--		   ,@mt_rsanio
	--		   ,@mt_rntipo
	--		   ,getdate()
	--		   ,'Insertada al cargar Matriculas Otros Consejos'
	--		   --     ,<rnCierra, int,>           ,<rnnamemod, varchar(50),>           ,<rnfechamod, datetime,>
	--		   ,@idUsuario)
	--	END TRY  
	--	BEGIN CATCH  
	--		SELECT   
	--			ERROR_NUMBER() AS ErrorNumber  
	--		   ,ERROR_MESSAGE() AS ErrorMessage;  
	--			SET @err_message = @err_message + ERROR_MESSAGE(); 
	--	END CATCH ;		  

	--	IF NOT EXISTS (Select [pfCodigo] from [dbo].[Profesiones]               where [pfNombre] = @nombreProfesion)       
	--			BEGIN 
	--			SET @err_message = @err_message  + ERROR_MESSAGE(); 
	--		END
	--	ELSE
	--	 BEGIN TRY
	--	 --tanto si ya existia como si se crearon
	--	-- SET @mtmatricula = (Select [mamatricula] from [dbo].[Matriculas] where [mamatricula]  = @mtmatricula  )
	--	 SET @idPersona = (Select  [psid] from [dbo].[Personas] where [psnumident] = @psnumident and [pstipoident] = @pstipoident)
	--	 SET @idProfesion = (Select [pfCodigo] from [dbo].[Profesiones]  where [pfNombre] = @nombreProfesion)
	--	 SET @idUniversidad =(Select [unCodigo] from [dbo].[Universidades] where [unNombre] = @nombreUniversidad)
	--		INSERT INTO [dbo].[InfoMatriculas]
	--		   ([mtmatricula]
	--		   ,[mtpersona]
	--		   ,[mtprofesion]
	--		   ,[mtuniversidad]
	--		   ,[mt_rsnumero]
	--		   ,[mt_rsanio]
	--		   ,[mt_rsseccional]
	--		   ,[mt_rnnumero]
	--		   ,[mt_rnanio]
	--		   ,[mt_rntipo]
	--		   ,[mtfechagrado]
	--		   ,[mtestado]
	--		   ,[idUsuario]
	--		   ,[flEstado]
	-- )
	--	 VALUES
	--		   (   
	--   @mtmatricula,
	--   @mtpersona ,
	--   @idProfesion,
	--   @idUniversidad ,
	--   @mt_rsnumero ,
	--   @mt_rsanio ,
	--   @mt_rsseccional ,
	--   @mt_rnnumero ,
	--   @mt_rnanio ,
	--   @mt_rntipo ,   
	--   @mtfechagrado ,
	--   @mtestado ,  
	--   @idUsuario ,
	--   @flestado    
	--   );
	--	 SET @mensaje= @mensaje + 'Se inserto exitosamente en Info Matriculas ' ;
	--END TRY  
	--	BEGIN CATCH  
	--		SELECT   
	--			ERROR_NUMBER() AS ErrorNumber  
	--		   ,ERROR_MESSAGE() AS ErrorMessage;  
	--			SET @err_message = @err_message + ERROR_MESSAGE(); 
	--			--Inserta en el Log de Auditoria
	--			INSERT INTO [Auditoria].[LogError]
	--		   ([logMensaje]           ,[logStackTrace]           ,[logFecha])
	--			 VALUES
	--		   ( @err_message           ,'tackTrace'           ,getdate())
	--	END CATCH;
END 