﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="xFilm5.SpeedBox.Web.Default" %>
<%@ Register Assembly="Gizmox.WebGUI.Forms" Namespace="Gizmox.WebGUI.Forms.Hosts" TagPrefix="vwg"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>xFilm SpeedBox</title>
    <meta name="description" content="xFilm5 SpeedBox PWA" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="/favicon.ico">

    <!-- Add manifest -->
    <link rel="manifest" href="/manifest.json">
    <!-- Tell the browser it's a PWA -->
    <meta name="mobile-web-app-capable" content="yes">
    <!-- Tell iOS it's a PWA -->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="theme-color" content="#536878">

    <link rel="apple-touch-icon" href="Resources/Images/ios-appicon-180-180.png">
    <link rel="apple-touch-icon" sizes="152x152" href="Resources/Images/ios-appicon-152-152.png">
    <link rel="apple-touch-icon" sizes="180x180" href="Resources/Images/ios-appicon-180-180.png">
    <!-- 
    @*<link rel="apple-touch-icon" sizes="167x167" href="Resources/Images/touch-icon-ipad-retina.png">*@
    -->
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <vwg:FormBox runat="server" Form="Film" Height="400" Width="480" Title="VWG" />
    </div>
    </form>
    <script>
      if ('serviceWorker' in navigator) {
        window.addEventListener('load', function() {
          navigator.serviceWorker.register('service-worker.js').then(function(registration) {
            // Registration was successful
            console.log('ServiceWorker registration successful with scope: ', registration.scope);
          }, function(err) {
            // registration failed :(
            console.log('ServiceWorker registration failed: ', err);
          }).catch(function(err) {
            console.log(err)
          });
        });
      } else {
        console.log('service worker is not supported');
      }
    </script>
</body>
</html>
