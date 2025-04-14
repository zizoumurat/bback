appcmd stop site api.buyersoft.com
xcopy "release" "C:\inetpub\wwwroot\api.buyersoft.com" /h /i /c /k /e /r /y
appcmd start site api.buyersoft.com