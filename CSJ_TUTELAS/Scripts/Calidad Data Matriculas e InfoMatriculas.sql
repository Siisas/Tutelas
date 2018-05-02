--Calidad Data Matriculas e InfoMatriculas
select count(*) from Matriculas --  479231  Registros en total   
--1. Data cleaning por Numero de Matricula
select * from Matriculas m left join InfoMatriculas i on m.mamatricula = i.mtmatricula
         where ltrim(rtrim(mamatricula)) in ( select mat	from (select mat, count(mat) as conteo from (
		 select ltrim(rtrim(mamatricula)) as mat from Matriculas as c1 ) a group by mat) b		where conteo >1)
		 and i.mtmatricula is null order by ltrim(rtrim(mamatricula)), mtmatricula  --7389 
--1. Resultado
select count(*) from InfoMatriculas--471640 7591 registros  menos por mamatriculas con un espacioadicional y valores nulos   (202)Por investigar
--2. Data Cleaning por Fecha Grado
select [mtfechagrado] ,dt.fecha
  FROM [COPNIA].[dbo].[InfoMatriculas] infm   left join [DWHCOPNIA].[dwh].[DIMTIEMPO] dt  on  [mtfechagrado] = dt.fecha
  where   dt.fecha is null --and [mtfechagrado] <> '1900-01-01'  --138975 Registros sin fecha de grado.
  union
  select count(*)
  FROM [COPNIA].[dbo].[InfoMatriculas] infm   left join [DWHCOPNIA].[dwh].[DIMTIEMPO] dt  on  [mtfechagrado] = dt.fecha
  where  [mtfechagrado] = '1900-01-01' --138470
-----------------------------------------------------------
  select [mtfechagrado] ,dt.fecha
  FROM [COPNIA].[dbo].[InfoMatriculas] infm   left join [DWHCOPNIA].[dwh].[DIMTIEMPO] dt  on  [mtfechagrado] = dt.fecha
  where   dt.fecha is null and [mtfechagrado] <> '1900-01-01'--6 registros
--2. Resultados
 select count(*) from InfoMatriculas where [mtfechagrado] = '1900-01-01'--138470 diferencia 505
  union  
  select count(*) from InfoMatriculas where [mtfechagrado] is null--499 diferencia  138476  
--3.Data Cleaning por ResolucionNacional
 select * from InfoMatriculas infm 
 left join [dbo].[ResNacional] rn 
 on infm.mt_rnnumero = rn.rnnumero
 and infm.mt_rnanio = rn.rnano 
 and infm.mt_rntipo = rn.rntipo --471640
 where rn.rnnumero is null 
  and rn.rnano is null 
  and rn.rntipo is null
--3.Resultado