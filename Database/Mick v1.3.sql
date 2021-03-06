USE [MickDatabase]
GO
/****** Object:  Table [dbo].[AboutUs]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TabNameId] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AboutUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Domain]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domain](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DomainInfor]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainInfor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NOT NULL,
	[TabNameId] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DomainInfor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestProject]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestProject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NULL,
	[Title] [nvarchar](250) NULL,
	[Content] [nvarchar](max) NULL,
	[Contact] [nvarchar](500) NULL,
 CONSTRAINT [PK_RequestProject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TabName]    Script Date: 9/24/2015 10:37:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TabName](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TabName] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Description]) VALUES (1, N'Industries', N'Industries')
INSERT [dbo].[Category] ([Id], [Name], [Description]) VALUES (2, N'Services', N'Services')
INSERT [dbo].[Category] ([Id], [Name], [Description]) VALUES (3, N'Product', N'Product')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Domain] ON 

INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (1, 1, N'Banking', N'Banking')
INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (2, 1, N'Education', N'Education')
INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (3, 1, N'Management', N'Management')
INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (4, 1, N'Business', N'Business')
INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (5, 1, N'Advertising', N'Advertising')
INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (6, 1, N'Transfer', N'Transfer')
INSERT [dbo].[Domain] ([Id], [SubCategoryId], [Name], [Description]) VALUES (7, 1, N'Computing', N'Computing')
SET IDENTITY_INSERT [dbo].[Domain] OFF
SET IDENTITY_INSERT [dbo].[DomainInfor] ON 

INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (1, 1, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (2, 2, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (3, 3, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (4, 4, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (5, 5, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (9, 6, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
INSERT [dbo].[DomainInfor] ([Id], [DomainId], [TabNameId], [Content]) VALUES (10, 7, 1, N'<div id="rs_digital_storage">
    <script language="JavaScript" src="/Scripts/BrightcoveExperiences.js" type="text/javascript" space="preserve"> </script>
    <div class="hpe_overlay video_bc" id="overlay_mp4_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587911" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <div class="hpe_overlay video_bc" id="overlay_mp9_video">
        <div class="video_wrapper">
            <!-- Begin of Brightcove Player -->
            <object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=640&amp;height=360&amp;flashID=myExperience2516054781001&amp;htmlFallback=true&amp;bgcolor=%23FFFFFF&amp;playerID=1377059119001&amp;playerKey=AQ~~%2CAAABAeI3VIE~%2CN0OfmZCPaxh-U75tF8pHH0iLtpzUxRz-&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;%40videoPlayer=2516054781001&amp;autoStart=&amp;debuggerID=&amp;startTime=1442027587912" id="myExperience2516054781001" width="640" height="360" class="BrightcoveExperience" seamlesstabbing="undefined">
                <param name="allowScriptAccess" value="always">
                <param name="allowFullScreen" value="true">
                <param name="seamlessTabbing" value="false">
                <param name="swliveconnect" value="true">
                <param name="wmode" value="window">
                <param name="quality" value="high">
                <param name="bgcolor" value="#FFFFFF">
            </object>
            <!-- End of Brightcove Player -->
        </div>
        <a class="hpe_overlay_cls" href="javascript:void(0);" shape="rect" style="text-decoration: none;">&nbsp;</a></div>
    <script type="text/javascript" space="preserve">                        brightcove.createExperiences();</script>
    <div class="pop_drk"></div>
    <!-- END VIDEOS -->
    <!-- start content integration -->
    <div class="breakout_recenter clearfix">
        <div class="hp_recommends"><strong xmlns="">HP recommends Windows.</strong></div>
        <div class="clearall"></div>
    </div>
    <div class="split_container">
        <h2><span class="f31 str">Capture customer attention and create an enhanced in-store experience</span></h2>
        <p><span class="final">Go big or go home. You’ve got to be bold and memorable for your brand to stand out from the crowd. Stunning HP Digital Signage and interactive solutions enable you to engage customers, personalize the experience and increase loyalty.</span></p>
    </div>
    <div class="split_container interactive_signage_display hairline_bottom">
        <div class="split_50 interactive_signage_display_content clearfix">
            <div class="left">
                <h2 class="m30">Performance Digital Signage Displays</h2>
                <p>See the difference on a dynamic, touch or non-touch 42 or 47-inch diagonal digital signage display. These displays include factory-integrated multi-touch, touch screens with advanced IR technology for accurate touch recognition and support of numerous gestures with no “ghosting”. They are plug and play and Windows® HID compliant for quick and easy installation.</p>
                <p><span class="str">Features</span></p>
                <ul class="bulleted_list_outside">
                    <li xmlns="">Video-over-Ethernet input </li>
                    <li>Embedded USB Media Sign Player</li>
                    <li>Designed for 24/7 play</li>
                    <li>Local or remote management</li>
                </ul>
                <div class="button_set"><a class="button inline primary" href="" rel="Learn more" shape="rect"><span class="btn_label">Learn more</span></a></div>
            </div>
            <div class="right">
                <div class="product_image">
                    <img xmlns="" alt="Performance Digital Signage Displays" src="/Content/Image/performance-hero_v3_tcm_245_1607399.jpg"></div>
            </div>
        </div>
    </div>
</div>')
SET IDENTITY_INSERT [dbo].[DomainInfor] OFF
SET IDENTITY_INSERT [dbo].[SubCategory] ON 

INSERT [dbo].[SubCategory] ([Id], [CategoryId], [Name], [Description]) VALUES (1, 1, N'Product Categories', N'Product Categories')
SET IDENTITY_INSERT [dbo].[SubCategory] OFF
SET IDENTITY_INSERT [dbo].[TabName] ON 

INSERT [dbo].[TabName] ([Id], [Name]) VALUES (1, N'tab1')
INSERT [dbo].[TabName] ([Id], [Name]) VALUES (2, N'tab2')
SET IDENTITY_INSERT [dbo].[TabName] OFF
ALTER TABLE [dbo].[AboutUs]  WITH CHECK ADD  CONSTRAINT [FK_AboutUs_TabName] FOREIGN KEY([TabNameId])
REFERENCES [dbo].[TabName] ([Id])
GO
ALTER TABLE [dbo].[AboutUs] CHECK CONSTRAINT [FK_AboutUs_TabName]
GO
ALTER TABLE [dbo].[Domain]  WITH CHECK ADD  CONSTRAINT [FK_Domain_Category] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[SubCategory] ([Id])
GO
ALTER TABLE [dbo].[Domain] CHECK CONSTRAINT [FK_Domain_Category]
GO
ALTER TABLE [dbo].[DomainInfor]  WITH CHECK ADD  CONSTRAINT [FK_DomainInfor_Domain] FOREIGN KEY([TabNameId])
REFERENCES [dbo].[TabName] ([Id])
GO
ALTER TABLE [dbo].[DomainInfor] CHECK CONSTRAINT [FK_DomainInfor_Domain]
GO
ALTER TABLE [dbo].[RequestProject]  WITH CHECK ADD  CONSTRAINT [FK_RequestProject_Domain] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domain] ([Id])
GO
ALTER TABLE [dbo].[RequestProject] CHECK CONSTRAINT [FK_RequestProject_Domain]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
