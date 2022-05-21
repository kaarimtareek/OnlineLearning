USE [OnlineLearning]
GO
INSERT [dbo].[LOOKUP_USER_STATUS] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'ACTIVE', N'نشط', N'Active', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LOOKUP_USER_STATUS] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'BLOCKED', N'محظور', N'Blocked', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LOOKUP_USER_STATUS] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'IN_ACTIVE', N'غير نشط', N'In active', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LOOKUP_USER_STATUS] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'PENDING', N'معلق', N'Pending', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomMeetingStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'ACTIVE', N'نشط', N'Active', CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomMeetingStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'CANCELED', N'ملغية', N'Canceled', CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomMeetingStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'CLOSED', N'مغلق', N'Closed', CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomMeetingStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'WAITING', N'قيد الانتظار', N'Waiting', CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), CAST(N'2021-11-28T08:06:37.3066414' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'ACTIVE', N'نشط', N'Active', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'CANCELED', N'ملغي', N'Canceled', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'FINISHED', N'منتهي', N'Finished', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'PENDING', N'معلق', N'Pending', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'ACCEPTED', N'مقبول', N'Accepted', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'CANCELED', N'لغي', N'Canceled', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'JOINED', N'إنضم', N'Joined', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'LEFT', N'رحل', N'Left', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'PENDING', N'معلق', N'Pending', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'REJECTED', N'مرفوض', N'Rejected', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
INSERT [dbo].[LookupUserRoomStatuses] ([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (N'SUSPENDED', N'موقوف', N'Suspended', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)
GO
