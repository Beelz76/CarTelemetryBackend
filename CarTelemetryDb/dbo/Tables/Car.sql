CREATE TABLE [dbo].[Car] (
    [CarId]                        INT              IDENTITY (1, 1) NOT NULL,
    [CarUid]                       UNIQUEIDENTIFIER NOT NULL,
    [UserId]                       INT              NOT NULL,
    [Name]                         NVARCHAR (256)   NOT NULL,
    [Image]                        NVARCHAR (256)   NULL,
    [Description]                  NVARCHAR (1024)   NOT NULL,
    [CurrentSpeed]                 FLOAT              NOT NULL,
    [MaxSpeed]                     FLOAT               NOT NULL,
    [AverageSpeed]                 FLOAT               NOT NULL,
    [CorneringSpeed]               FLOAT               NOT NULL,
    [MaxAcceleration]              FLOAT               NOT NULL,
    [AverageAcceleration]          FLOAT               NOT NULL,
    [MaxBraking]                   FLOAT               NOT NULL,
    [AverageBraking]               FLOAT               NOT NULL,
    [EngineSpeed]                  FLOAT               NOT NULL,
    [EnginePower]                  FLOAT               NOT NULL,
    [SuspensionVibrationAmplitude] FLOAT               NOT NULL,
    [SuspensionVibrationSpeed]     FLOAT               NOT NULL,
    PRIMARY KEY CLUSTERED ([CarId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED ([CarUid] ASC)
);

