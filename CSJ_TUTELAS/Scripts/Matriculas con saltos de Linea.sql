--Matriculas con saltos de Linea
SELECT distinct rtrim(ltrim( replace([mamatricula], '
', '') ))
      ,[maestado]
      ,[manumero]
      ,[matipo]
      ,[idUsuario]
      ,[idHomologacion]
  FROM [DWHCOPNIA].[dwh].[DIMMATRICULA]
  where [mamatricula] like '08202-28571%'

  SELECT distinct rtrim(ltrim([mamatricula]))
      ,[maestado]
      ,[manumero]
      ,[matipo]
      ,[idUsuario]
      ,[idHomologacion]
  FROM [DWHCOPNIA].[dwh].[DIMMATRICULA]
  where [mamatricula] = '08202-28571
'
  SELECT distinct rtrim(ltrim([mamatricula]))
      ,[maestado]
      ,[manumero]
      ,[matipo]
      ,[idUsuario]
      ,[idHomologacion]
  FROM [DWHCOPNIA].[dwh].[DIMMATRICULA]
  where [mamatricula] = '08202-28571' '08202-28571'


  select replace([mamatricula], '
', '')  FROM [DWHCOPNIA].[dwh].[DIMMATRICULA]
  where [mamatricula] = '08202-28571
'
 