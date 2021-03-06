
USE [SIGACUN]
GO
/****** Object:  Table [dbo].[estados]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[estados]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[estados](
	[est_id] [int] NOT NULL,
	[est_descripcion] [varchar](50) NULL,
	[est_hd_estado] [int] NULL,
 CONSTRAINT [PK_estados] PRIMARY KEY CLUSTERED 
(
	[est_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (0, NULL, NULL)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (1, N'Sin revisar', NULL)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (2, N'En Espera', 1)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (3, N'En Progreso', 1)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (4, N'Terminado', 1)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (5, N'Evaluado', NULL)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (6, N'Trasladada', NULL)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (7, N'Rechazada', NULL)
INSERT [dbo].[estados] ([est_id], [est_descripcion], [est_hd_estado]) VALUES (8, N'Cancelada', NULL)
/****** Object:  Table [dbo].[cargos]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cargos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cargos](
	[car_id] [int] IDENTITY(1,1) NOT NULL,
	[car_nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cargos] PRIMARY KEY CLUSTERED 
(
	[car_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cargos] ON
INSERT [dbo].[cargos] ([car_id], [car_nombre]) VALUES (1, N'Nada')
SET IDENTITY_INSERT [dbo].[cargos] OFF
/****** Object:  Table [dbo].[areas]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[areas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[areas](
	[are_id] [int] NOT NULL,
	[are_nombre] [varchar](60) NOT NULL,
	[are_hd_area] [int] NOT NULL,
	[are_supervisor] [int] NULL,
 CONSTRAINT [PK_areas] PRIMARY KEY CLUSTERED 
(
	[are_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[areas] ([are_id], [are_nombre], [are_hd_area], [are_supervisor]) VALUES (0, N'Ninguno', 1, 0)
/****** Object:  Table [dbo].[prioridad]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[prioridad]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[prioridad](
	[pri_id] [int] NOT NULL,
	[pri_nombre] [varchar](30) NOT NULL,
	[pri_hd_prioridad] [int] NOT NULL,
 CONSTRAINT [PK_prioridad_1] PRIMARY KEY CLUSTERED 
(
	[pri_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[prioridad] ([pri_id], [pri_nombre], [pri_hd_prioridad]) VALUES (1, N'Baja', 0)
INSERT [dbo].[prioridad] ([pri_id], [pri_nombre], [pri_hd_prioridad]) VALUES (2, N'Media', 0)
INSERT [dbo].[prioridad] ([pri_id], [pri_nombre], [pri_hd_prioridad]) VALUES (3, N'Alta', 0)
/****** Object:  Table [dbo].[fechasfestivo]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fechasfestivo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fechasfestivo](
	[fec_id] [int] IDENTITY(1,1) NOT NULL,
	[fec_dia] [date] NOT NULL,
	[fec_tipo] [int] NULL,
 CONSTRAINT [PK_fechasfestivo_1] PRIMARY KEY CLUSTERED 
(
	[fec_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fechasfestivo] ON
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (1, CAST(0x4C320B00 AS Date), 1)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (2, CAST(0xC4320B00 AS Date), 1)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (3, CAST(0x14330B00 AS Date), 1)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (4, CAST(0x26330B00 AS Date), 1)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (5, CAST(0xA1330B00 AS Date), 1)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (6, CAST(0xB2330B00 AS Date), 1)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (7, CAST(0x51320B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (8, CAST(0x99320B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (9, CAST(0xFF320B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (11, CAST(0x2E330B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (12, CAST(0x68330B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (13, CAST(0x7C330B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (14, CAST(0x86330B00 AS Date), 2)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (15, CAST(0xA6320B00 AS Date), 3)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (16, CAST(0xA7320B00 AS Date), 3)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (17, CAST(0xD4320B00 AS Date), 3)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (18, CAST(0xE9320B00 AS Date), 3)
INSERT [dbo].[fechasfestivo] ([fec_id], [fec_dia], [fec_tipo]) VALUES (19, CAST(0xF0320B00 AS Date), 3)
SET IDENTITY_INSERT [dbo].[fechasfestivo] OFF
/****** Object:  Table [dbo].[evapreguntas]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[evapreguntas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[evapreguntas](
	[epr_id] [int] NOT NULL,
	[epre_pregunta] [text] NOT NULL,
 CONSTRAINT [PK_evapreguntas] PRIMARY KEY CLUSTERED 
(
	[epr_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[roles]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[roles](
	[rol_id] [int] IDENTITY(1,1) NOT NULL,
	[rol_descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_roles_1] PRIMARY KEY CLUSTERED 
(
	[rol_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON
INSERT [dbo].[roles] ([rol_id], [rol_descripcion]) VALUES (1, N'Admin')
INSERT [dbo].[roles] ([rol_id], [rol_descripcion]) VALUES (2, N'Supervisor')
INSERT [dbo].[roles] ([rol_id], [rol_descripcion]) VALUES (3, N'Empleado')
SET IDENTITY_INSERT [dbo].[roles] OFF
/****** Object:  Table [dbo].[usuarios]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usuarios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[usuarios](
	[usu_id] [int] NOT NULL,
	[usu_username] [varchar](50) NOT NULL,
	[usu_contrasena] [varchar](50) NOT NULL,
	[usu_nombres] [varchar](25) NOT NULL,
	[usu_apellidos] [varchar](50) NOT NULL,
	[usu_activo] [tinyint] NOT NULL,
	[usu_hd_usuario] [int] NOT NULL,
	[car_id] [int] NOT NULL,
	[are_id] [int] NOT NULL,
	[rol_id] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[usu_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'usuarios', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Campo para indentificar si es supervisor del area' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'usuarios'
GO
INSERT [dbo].[usuarios] ([usu_id], [usu_username], [usu_contrasena], [usu_nombres], [usu_apellidos], [usu_activo], [usu_hd_usuario], [car_id], [are_id], [rol_id]) VALUES (0, N'ADMIN', N' ,¹b¬Y[–K-#Kp', N'ADMIN', N'ADMIN', 0, 1, 1, 0, 1)
/****** Object:  Table [dbo].[procedimiento]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procedimiento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[procedimiento](
	[pro_id] [int] IDENTITY(1,1) NOT NULL,
	[pro_codigoun] [varchar](30) NOT NULL,
	[pro_nombre] [varchar](50) NOT NULL,
	[pro_describcion] [varchar](250) NULL,
	[pro_activo] [tinyint] NOT NULL,
	[are_id] [int] NOT NULL,
 CONSTRAINT [PK_procedimiento_1] PRIMARY KEY CLUSTERED 
(
	[pro_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[procedimiento] ON
INSERT [dbo].[procedimiento] ([pro_id], [pro_codigoun], [pro_nombre], [pro_describcion], [pro_activo], [are_id]) VALUES (4, N'SIN ASIGNAR', N'SIN ASIGNAR', N'SIN ASIGNAR', 1, 0)
SET IDENTITY_INSERT [dbo].[procedimiento] OFF
/****** Object:  Table [dbo].[actividades_hd]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[actividades_hd]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[actividades_hd](
	[ahd_id] [int] IDENTITY(1,1) NOT NULL,
	[ahd_hd_numsolicitud] [int] NOT NULL,
	[ahd_bloque] [varchar](250) NULL,
	[ahd_piso] [varchar](250) NULL,
	[ahd_ubicacion] [varchar](250) NULL,
	[ahd_descripcion] [text] NOT NULL,
	[ahd_fhpeticion] [datetime] NOT NULL,
	[ahd_fhasignacion] [datetime] NOT NULL,
	[ahd_solicitante] [varchar](250) NOT NULL,
	[ahd_fhfin] [datetime] NULL,
	[ahd_duracion] [real] NULL,
	[ahd_duratotal] [real] NULL,
	[ahd_comentario] [text] NULL,
	[ahd_fhcomentario] [datetime] NULL,
	[ahd_comsuper] [text] NULL,
	[ahd_fhcomsuper] [datetime] NULL,
	[pri_id] [int] NOT NULL,
	[usu_id] [int] NOT NULL,
	[pro_id] [int] NOT NULL,
	[est_id] [int] NULL,
 CONSTRAINT [PK_actividades_hd] PRIMARY KEY CLUSTERED 
(
	[ahd_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Llave principal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_hd_numsolicitud'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'numero de solicitud de la mesa de ayuda' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_hd_numsolicitud'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_descripcion'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'descripcion del caso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_descripcion'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_fhpeticion'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha y hora de peticion de solicitud' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_fhpeticion'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_fhasignacion'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha y hora de asignacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_fhasignacion'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_solicitante'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Solicitande o usuario que pidio el caso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_solicitante'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_fhfin'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha terminacion del caso.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_fhfin'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_duracion'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'duracion de asignacion a terminacion del caso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_duracion'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_duratotal'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'duracion de solicitud a terminacion del caso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_duratotal'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_comentario'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'comentario del empleado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_comentario'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_fhcomentario'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha y hora del comentario por parte del empleado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_fhcomentario'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_comsuper'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario del supervisor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_comsuper'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'ahd_fhcomsuper'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha y hora de comentario del supervisor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'ahd_fhcomsuper'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'pri_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'llave de la prioridad del caso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'pri_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'usu_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'empleado ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'usu_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'actividades_hd', N'COLUMN',N'pro_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'actividades_hd', @level2type=N'COLUMN',@level2name=N'pro_id'
GO
/****** Object:  Table [dbo].[actividades]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[actividades]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[actividades](
	[act_id] [int] IDENTITY(1,1) NOT NULL,
	[act_nombre] [varchar](50) NOT NULL,
	[act_descripcion] [varchar](250) NULL,
	[act_activo] [int] NOT NULL,
	[act_operacion] [int] NULL,
	[pro_id] [int] NOT NULL,
 CONSTRAINT [PK_actividades] PRIMARY KEY CLUSTERED 
(
	[act_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[archivospro]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[archivospro]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[archivospro](
	[arp_id] [int] IDENTITY(1,1) NOT NULL,
	[arp_nombre] [text] NOT NULL,
	[arp_fechacrea] [datetime] NOT NULL,
	[pro_id] [int] NOT NULL,
 CONSTRAINT [PK_archivospro] PRIMARY KEY CLUSTERED 
(
	[arp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[asignaciones]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[asignaciones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[asignaciones](
	[asi_id] [int] IDENTITY(1,1) NOT NULL,
	[asi_descripcion] [text] NULL,
	[asi_fhasignacion] [datetime] NOT NULL,
	[asi_fechaterminar] [date] NOT NULL,
	[asi_solicitante] [varchar](250) NOT NULL,
	[asi_fhinicio] [datetime] NULL,
	[asi_fhfin] [datetime] NULL,
	[asi_comentario] [text] NULL,
	[asi_hfcomentario] [datetime] NULL,
	[asi_comsuper] [text] NULL,
	[asi_hfcomesuper] [datetime] NULL,
	[asi_operador] [real] NULL,
	[act_id] [int] NOT NULL,
	[use_id] [int] NOT NULL,
	[pri_id] [int] NULL,
	[est_id] [int] NULL,
 CONSTRAINT [PK_asignaciones] PRIMARY KEY CLUSTERED 
(
	[asi_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'asignaciones', N'COLUMN',N'asi_comentario'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario empleado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asignaciones', @level2type=N'COLUMN',@level2name=N'asi_comentario'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'asignaciones', N'COLUMN',N'asi_hfcomentario'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora comentario empleado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asignaciones', @level2type=N'COLUMN',@level2name=N'asi_hfcomentario'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'asignaciones', N'COLUMN',N'asi_comsuper'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario supervisor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asignaciones', @level2type=N'COLUMN',@level2name=N'asi_comsuper'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'asignaciones', N'COLUMN',N'asi_hfcomesuper'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora comentario supervisor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asignaciones', @level2type=N'COLUMN',@level2name=N'asi_hfcomesuper'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'asignaciones', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asignaciones'
GO
/****** Object:  Table [dbo].[evaluacion]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[evaluacion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[evaluacion](
	[eva_id] [int] IDENTITY(1,1) NOT NULL,
	[eva_observacion] [text] NULL,
	[ahd_id] [int] NOT NULL,
	[epr_id] [int] NULL,
	[eva_calificacion] [real] NULL,
 CONSTRAINT [PK_evaluacion] PRIMARY KEY CLUSTERED 
(
	[eva_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[archivosact]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[archivosact]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[archivosact](
	[ara_id] [int] IDENTITY(1,1) NOT NULL,
	[ara_nombre] [text] NOT NULL,
	[ara_fecha] [datetime] NOT NULL,
	[act_id] [int] NOT NULL,
 CONSTRAINT [PK_archivosact] PRIMARY KEY CLUSTERED 
(
	[ara_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[archivohd]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[archivohd]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[archivohd](
	[arh_id] [int] IDENTITY(1,1) NOT NULL,
	[arh_nombre] [int] NOT NULL,
	[arch_fecha] [datetime] NOT NULL,
	[ahd_id] [int] NOT NULL,
 CONSTRAINT [PK_archivohd] PRIMARY KEY CLUSTERED 
(
	[arh_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[repeticiones]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[repeticiones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[repeticiones](
	[rep_id] [int] IDENTITY(1,1) NOT NULL,
	[rep_fechainicio] [date] NULL,
	[rep_fechafin] [date] NULL,
	[rep_cadacuando] [int] NULL,
	[rep_cantidad] [int] NULL,
	[asi_id] [int] NOT NULL,
 CONSTRAINT [PK_repeticiones] PRIMARY KEY CLUSTERED 
(
	[rep_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarEvalucion]    Script Date: 07/30/2010 10:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertarEvalucion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertarEvalucion]
(
    @ahd_id int,
    @epr_id int,
    @calificacion int,
    @observacion text
)
AS
    INSERT INTO evaluacion
    (
        ahd_id,
        epr_id,
        eva_calificacion,
        eva_observacion

    )
    VALUES
    (
       @ahd_id,
       @epr_id,
       @calificacion,
	   @observacion
    )
' 
END
GO
/****** Object:  Table [dbo].[archivosasig]    Script Date: 07/30/2010 10:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[archivosasig]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[archivosasig](
	[ars_id] [int] IDENTITY(1,1) NOT NULL,
	[ars_nombre] [text] NOT NULL,
	[ars_fecha] [datetime] NOT NULL,
	[asi_id] [int] NOT NULL,
 CONSTRAINT [PK_archivosasig] PRIMARY KEY CLUSTERED 
(
	[ars_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_actividades_procedimiento1]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_procedimiento1]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades]'))
ALTER TABLE [dbo].[actividades]  WITH CHECK ADD  CONSTRAINT [FK_actividades_procedimiento1] FOREIGN KEY([pro_id])
REFERENCES [dbo].[procedimiento] ([pro_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_procedimiento1]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades]'))
ALTER TABLE [dbo].[actividades] CHECK CONSTRAINT [FK_actividades_procedimiento1]
GO
/****** Object:  ForeignKey [FK_actividades_hd_estados]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_estados]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd]  WITH CHECK ADD  CONSTRAINT [FK_actividades_hd_estados] FOREIGN KEY([est_id])
REFERENCES [dbo].[estados] ([est_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_estados]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd] CHECK CONSTRAINT [FK_actividades_hd_estados]
GO
/****** Object:  ForeignKey [FK_actividades_hd_prioridad]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_prioridad]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd]  WITH CHECK ADD  CONSTRAINT [FK_actividades_hd_prioridad] FOREIGN KEY([pri_id])
REFERENCES [dbo].[prioridad] ([pri_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_prioridad]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd] CHECK CONSTRAINT [FK_actividades_hd_prioridad]
GO
/****** Object:  ForeignKey [FK_actividades_hd_procedimiento]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_procedimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd]  WITH CHECK ADD  CONSTRAINT [FK_actividades_hd_procedimiento] FOREIGN KEY([pro_id])
REFERENCES [dbo].[procedimiento] ([pro_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_procedimiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd] CHECK CONSTRAINT [FK_actividades_hd_procedimiento]
GO
/****** Object:  ForeignKey [FK_actividades_hd_usuarios]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd]  WITH CHECK ADD  CONSTRAINT [FK_actividades_hd_usuarios] FOREIGN KEY([usu_id])
REFERENCES [dbo].[usuarios] ([usu_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_actividades_hd_usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[actividades_hd]'))
ALTER TABLE [dbo].[actividades_hd] CHECK CONSTRAINT [FK_actividades_hd_usuarios]
GO
/****** Object:  ForeignKey [FK_archivohd_actividades_hd]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivohd_actividades_hd]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivohd]'))
ALTER TABLE [dbo].[archivohd]  WITH CHECK ADD  CONSTRAINT [FK_archivohd_actividades_hd] FOREIGN KEY([ahd_id])
REFERENCES [dbo].[actividades_hd] ([ahd_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivohd_actividades_hd]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivohd]'))
ALTER TABLE [dbo].[archivohd] CHECK CONSTRAINT [FK_archivohd_actividades_hd]
GO
/****** Object:  ForeignKey [FK_archivosact_actividades]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivosact_actividades]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivosact]'))
ALTER TABLE [dbo].[archivosact]  WITH CHECK ADD  CONSTRAINT [FK_archivosact_actividades] FOREIGN KEY([act_id])
REFERENCES [dbo].[actividades] ([act_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivosact_actividades]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivosact]'))
ALTER TABLE [dbo].[archivosact] CHECK CONSTRAINT [FK_archivosact_actividades]
GO
/****** Object:  ForeignKey [FK_archivosasig_asignaciones]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivosasig_asignaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivosasig]'))
ALTER TABLE [dbo].[archivosasig]  WITH CHECK ADD  CONSTRAINT [FK_archivosasig_asignaciones] FOREIGN KEY([asi_id])
REFERENCES [dbo].[asignaciones] ([asi_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivosasig_asignaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivosasig]'))
ALTER TABLE [dbo].[archivosasig] CHECK CONSTRAINT [FK_archivosasig_asignaciones]
GO
/****** Object:  ForeignKey [FK_archivospro_procedimiento1]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivospro_procedimiento1]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivospro]'))
ALTER TABLE [dbo].[archivospro]  WITH CHECK ADD  CONSTRAINT [FK_archivospro_procedimiento1] FOREIGN KEY([pro_id])
REFERENCES [dbo].[procedimiento] ([pro_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_archivospro_procedimiento1]') AND parent_object_id = OBJECT_ID(N'[dbo].[archivospro]'))
ALTER TABLE [dbo].[archivospro] CHECK CONSTRAINT [FK_archivospro_procedimiento1]
GO
/****** Object:  ForeignKey [FK_asignaciones_actividades]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_actividades]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones]  WITH CHECK ADD  CONSTRAINT [FK_asignaciones_actividades] FOREIGN KEY([act_id])
REFERENCES [dbo].[actividades] ([act_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_actividades]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones] CHECK CONSTRAINT [FK_asignaciones_actividades]
GO
/****** Object:  ForeignKey [FK_asignaciones_estados]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_estados]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones]  WITH CHECK ADD  CONSTRAINT [FK_asignaciones_estados] FOREIGN KEY([est_id])
REFERENCES [dbo].[estados] ([est_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_estados]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones] CHECK CONSTRAINT [FK_asignaciones_estados]
GO
/****** Object:  ForeignKey [FK_asignaciones_prioridad]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_prioridad]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones]  WITH CHECK ADD  CONSTRAINT [FK_asignaciones_prioridad] FOREIGN KEY([pri_id])
REFERENCES [dbo].[prioridad] ([pri_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_prioridad]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones] CHECK CONSTRAINT [FK_asignaciones_prioridad]
GO
/****** Object:  ForeignKey [FK_asignaciones_usuarios]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones]  WITH CHECK ADD  CONSTRAINT [FK_asignaciones_usuarios] FOREIGN KEY([use_id])
REFERENCES [dbo].[usuarios] ([usu_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asignaciones_usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[asignaciones]'))
ALTER TABLE [dbo].[asignaciones] CHECK CONSTRAINT [FK_asignaciones_usuarios]
GO
/****** Object:  ForeignKey [FK_evaluacion_actividades_hd]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_evaluacion_actividades_hd]') AND parent_object_id = OBJECT_ID(N'[dbo].[evaluacion]'))
ALTER TABLE [dbo].[evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_evaluacion_actividades_hd] FOREIGN KEY([ahd_id])
REFERENCES [dbo].[actividades_hd] ([ahd_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_evaluacion_actividades_hd]') AND parent_object_id = OBJECT_ID(N'[dbo].[evaluacion]'))
ALTER TABLE [dbo].[evaluacion] CHECK CONSTRAINT [FK_evaluacion_actividades_hd]
GO
/****** Object:  ForeignKey [FK_evaluacion_evapreguntas]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_evaluacion_evapreguntas]') AND parent_object_id = OBJECT_ID(N'[dbo].[evaluacion]'))
ALTER TABLE [dbo].[evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_evaluacion_evapreguntas] FOREIGN KEY([epr_id])
REFERENCES [dbo].[evapreguntas] ([epr_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_evaluacion_evapreguntas]') AND parent_object_id = OBJECT_ID(N'[dbo].[evaluacion]'))
ALTER TABLE [dbo].[evaluacion] CHECK CONSTRAINT [FK_evaluacion_evapreguntas]
GO
/****** Object:  ForeignKey [FK_procedimiento_areas1]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_procedimiento_areas1]') AND parent_object_id = OBJECT_ID(N'[dbo].[procedimiento]'))
ALTER TABLE [dbo].[procedimiento]  WITH CHECK ADD  CONSTRAINT [FK_procedimiento_areas1] FOREIGN KEY([are_id])
REFERENCES [dbo].[areas] ([are_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_procedimiento_areas1]') AND parent_object_id = OBJECT_ID(N'[dbo].[procedimiento]'))
ALTER TABLE [dbo].[procedimiento] CHECK CONSTRAINT [FK_procedimiento_areas1]
GO
/****** Object:  ForeignKey [FK_repeticiones_asignaciones]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_repeticiones_asignaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[repeticiones]'))
ALTER TABLE [dbo].[repeticiones]  WITH CHECK ADD  CONSTRAINT [FK_repeticiones_asignaciones] FOREIGN KEY([asi_id])
REFERENCES [dbo].[asignaciones] ([asi_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_repeticiones_asignaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[repeticiones]'))
ALTER TABLE [dbo].[repeticiones] CHECK CONSTRAINT [FK_repeticiones_asignaciones]
GO
/****** Object:  ForeignKey [FK_usuarios_areas1]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usuarios_areas1]') AND parent_object_id = OBJECT_ID(N'[dbo].[usuarios]'))
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_areas1] FOREIGN KEY([are_id])
REFERENCES [dbo].[areas] ([are_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usuarios_areas1]') AND parent_object_id = OBJECT_ID(N'[dbo].[usuarios]'))
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_areas1]
GO
/****** Object:  ForeignKey [FK_usuarios_cargos2]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usuarios_cargos2]') AND parent_object_id = OBJECT_ID(N'[dbo].[usuarios]'))
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_cargos2] FOREIGN KEY([car_id])
REFERENCES [dbo].[cargos] ([car_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usuarios_cargos2]') AND parent_object_id = OBJECT_ID(N'[dbo].[usuarios]'))
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_cargos2]
GO
/****** Object:  ForeignKey [FK_usuarios_roles]    Script Date: 07/30/2010 10:20:00 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usuarios_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[usuarios]'))
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[roles] ([rol_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usuarios_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[usuarios]'))
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_roles]
GO
