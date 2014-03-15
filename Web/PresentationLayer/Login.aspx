<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PresentationLayer.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/assets/css/flatly.bootstrap.css" rel="stylesheet" type="text/css" />
    <%--<link href="/assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="/assets/css/login.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    <%--#flash-message .alert{padding:18px;}--%>
    </style>
    <script src="/assets/js/jquery-1.10.2.min.js" type="text/javascript" ></script>
    <script src="/assets/js/jquery.extensions.js" type="text/javascript"></script>
</head>
<body>
    <div id="login" class="well">
    <form id="form1" runat="server">
            
        <fieldset>
            <table style="width:100%;margin:0 0 20px 0;">
                <tr>
                    <td>
                    <div id="rotating_pencils">
                    <style type="text/css">
                        #swatches{
                            animation-name: ckw;
                            animation-duration: 0s;
                            /* Things added */
                            animation-iteration-count: infinite;
                            transform-origin: 50% 50%;
                            display: inline-block;
                            /* <--- */
                        }
                        @keyframes ckw {
                            0% {
                                transform: rotate(0deg);
                            }
                            100% {
                                transform: rotate(-360deg);
                            }
                        }
                        
                    </style>
					<%--<span class="svg">
                        <svg xml:space="preserve" enable-background="new 0 0 100 100" viewBox="0 0 100 100" height="35px" width="35px" y="0px" x="0px" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns="http://www.w3.org/2000/svg" id="swatches" version="1.1">
							<g transform=" rotate(-1260,50,50)" id="pen-1">
								<path d="M50,74.001V100h0.072c-0.023,0-0.049,0.002-0.072,0.002c-12.816-0.008-25.662-4.828-35.417-14.584C4.812,75.646-0.008,62.838,0,50c0.009-12.816,4.829-25.662,14.583-35.417l0,0L33,33c-4.682,4.683-6.996,10.849-7,17c-0.004,6.162,2.31,12.311,7,17C37.683,71.684,43.848,73.996,50,74.001z" fill="rgb(41,187,156)" id="body-1"/>
								<path d="M50,92C26.804,92,8,73.195,8,50c0-11.598,4.701-22.098,12.302-29.699l-5.719-5.718C7.868,21.299,0,33.438,0,50c0,27.062,21.625,50,50,50V92z" enable-background="new    " fill="#000000" fill-opacity="0.15" id="outer-1"/>
								<path d="M32.928,67c-4.689-4.689-7.003-10.838-7-17c0.004-6.151,2.316-12.317,7-17l-5.627-5.627c-5.791,5.79-9.373,13.79-9.373,22.627c0,17.673,14.327,32,32,32v-7.999C43.776,73.996,37.612,71.684,32.928,67z" enable-background="new    " fill="#FFFFFF" fill-opacity="0.15" id="inner-1"/>
								<path d="M63,87L50.014,74.009V74c-0.003,0-0.005,0.001-0.008,0.001L50.005,74L50,74.001c-3.309,0.011-6.009,5.826-6.009,13c0,7.181,2.709,13.001,6.022,13.001V99.98L63,87z" fill="#EFC22D" id="wood-1"/>
								<path d="M58.758,82.757C57.672,83.843,57,85.343,57,87c0,1.656,0.672,3.156,1.757,4.242L63,87L58.758,82.757z" fill="#2D3E4F" id="stylus-1"/>
							</g>
                            <g transform=" rotate(-1260,50,50)" id="pen-2">
								<path d="M50,26.001V0.002h-0.072C49.951,0.002,49.976,0,50,0c12.816,0.008,25.662,4.828,35.417,14.584c9.771,9.771,14.592,22.58,14.584,35.417c-0.009,12.817-4.829,25.663-14.584,35.418l0,0L66.999,67.002c4.683-4.682,6.996-10.85,7-17c0.004-6.162-2.312-12.31-7-17C62.316,28.319,56.15,26.006,50,26.001z" fill="rgb(229,77,66)" id="body-2"/>
								<path d="M66.999,33.002c4.688,4.689,7.004,10.838,7,17c-0.004,6.15-2.317,12.318-7,17l5.627,5.627c5.791-5.789,9.373-13.789,9.373-22.627c0-17.672-14.326-32-32-32v7.999C56.15,26.006,62.316,28.319,66.999,33.002z" enable-background="new    " fill="#000000" fill-opacity="0.1" id="inner-2"/>
								<path d="M50.001,8.001c23.195,0,41.999,18.805,41.999,42C92,61.6,87.299,72.1,79.698,79.7l5.72,5.718C92.133,78.702,100,66.563,100,50.001c0-27.062-21.624-50-49.999-50V8.001z" enable-background="new    " fill="#FFFFFF" fill-opacity="0.15" id="outer-2"/>
								<path d="M37,13.002l12.986,12.991v0.009c0.003,0,0.006-0.001,0.008-0.001l0.001,0.001L50,26.001c3.309-0.011,6.008-5.826,6.008-13C56.008,5.821,53.299,0,49.986,0v0.021L37,13.002z" fill="#EFC22D" id="wood-2"/>
								<path d="M41.242,17.245C42.328,16.159,43,14.659,43,13.002c0-1.656-0.672-3.156-1.757-4.242L37,13.002L41.242,17.245z" fill="#2D3E4F" id="stylus-2"/>
							</g>
						</svg>
                    </span>--%>
				</div>
                    <%--<img src="/assets/icons/icon_list_board.png" style="width:50px;"/>--%>
                    </td>
                    <td><h3>Logic University</h3></td>
                </tr>
            </table>
            <div id="flash-message"></div>
            <script type="text/javascript">
                $(function () { $("#flash-message").flashMessage(); });
            </script>
            
            <div class="control-group">
            <!-- Username -->
            <%--<label class="control-label"  for="username">Username</label>--%>
            <div class="controls">
                <asp:TextBox ID="ui_username_textbox" runat="server" placeholder="Username" style="height:25px;width:280px;"></asp:TextBox>
            </div>
            </div>
 
            <div class="control-group">
            <!-- Password-->

            <%--<label class="control-label" for="password">Password</label>--%>
            
            <div class="controls">
                <asp:TextBox ID="ui_password_textbox" runat="server" TextMode="Password" placeholder="Password" style="height:25px;width:280px;"></asp:TextBox>
            </div>
            </div>
 
 
            <div class="control-group">
            <!-- Button -->
            <div class="controls" style="text-align:left;">
            
            <asp:Button ID="Button1" runat="server" onclick="actionLoginButtonClicked" 
                    Text="Login" class="btn btn-primary btn-medium" />
            </div>
            </div>
        </fieldset> 
    </form>
    </div>
</body>
</html>
