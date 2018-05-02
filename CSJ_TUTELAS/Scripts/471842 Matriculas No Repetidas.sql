--471842 Matriculas No Repetidas
SELECT [mamatricula]
      ,[maestado]
      ,[manumero]
      ,[matipo]
      ,[idUsuario]
      ,[idHomologacion]
  FROM [dbo].[Matriculas]
  except
select m.[mamatricula]
      ,m.[maestado]
      ,m.[manumero]
      ,m.[matipo]
      ,m.[idUsuario]
      ,m.[idHomologacion] 
	  
	   from Matriculas m left join InfoMatriculas i on m.mamatricula = i.mtmatricula
         where ltrim(rtrim(mamatricula)) in ( select mat	from (select mat, count(mat) as conteo from (
		 select ltrim(rtrim(mamatricula)) as mat from Matriculas as c1 ) a group by mat) b		where conteo >1)
		 and i.mtmatricula is null-- order by ltrim(rtrim(mamatricula)), mtmatricula  )