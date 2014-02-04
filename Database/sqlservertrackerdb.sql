USE [tracker]
GO

/****** Object:  Table [dbo].[trackinglog]    Script Date: 02/03/2014 20:50:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[trackinglog](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[TimeReceived] [datetime] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Speed] [float] NOT NULL,
	[StatusBit] [varchar](1) NOT NULL,
	[QtyLt] [float] NOT NULL,
	[StatusBitTank1] [varchar](1) NULL,
	[QtyLtTank1] [float] NULL,
	[StatusBitTank2] [varchar](1) NULL,
	[QtyLtTank2] [float] NULL,
 CONSTRAINT [PK_trackinglog] PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


