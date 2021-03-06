--Estadisticas Cargue Excel insInfoMatriculas

SELECT * FROM [COPNIA].[dbo].[InfoMatriculas]			WHERE [mtmatricula] like '%A018%'

SELECT * from [COPNIA].[dbo].[Matriculas]				WHERE [mamatricula] like '%A018%'

SELECT *  FROM [COPNIA].[dbo].[Personas]  p inner join [COPNIA].[dbo].[InfoMatriculas] i
on p.psid = i.mtpersona where i.mtmatricula like '%A018%'	order by [psid] desc

SELECT *  FROM [COPNIA].[dbo].[Profesiones]	p inner join [COPNIA].[dbo].[InfoMatriculas] i
on p.pfCodigo = i.mtprofesion where i.mtmatricula like '%A018%'	order by [pfCodigo] desc

SELECT *  FROM [COPNIA].[dbo].[Universidades] u inner join [COPNIA].[dbo].[InfoMatriculas] i
on u.unCodigo = i.mtprofesion where i.mtmatricula like '%A018%'				order by [unCodigo] desc
--Select [unCodigo] from [dbo].[Universidades] where [unNombre] = 'UNIVERSIDAD DEL CAUCA'
SELECT *  FROM [COPNIA].[dbo].[ResNacional] rn inner join [COPNIA].[dbo].[InfoMatriculas] i
on rn.rnnumero  = i.mt_rnnumero and rn.rntipo = i.mt_rntipo and rn.rnano = i.mt_rnanio
 where i.mtmatricula like '%A018%'					order by [rnnumero] desc--,[rnano],[rntipo] asc
--Select [rnnumero], [rnano], [rntipo] from [dbo].[ResNacional] where [rnnumero] = 650141 and [rnano] = 2015 and [rntipo] = 2
SELECT *  FROM [COPNIA].[dbo].[ResolucionSeccional] rs inner join [COPNIA].[dbo].[InfoMatriculas] i
on rs.rsnumero  = i.mt_rsnumero and rs.rsseccional = i.mt_rsseccional and rs.rsanio = i.mt_rsanio
 where i.mtmatricula like '%A018%'		order by [rsnumero] desc

SELECT * from  [COPNIA].[Auditoria].[LogError]	order by idLogError desc
