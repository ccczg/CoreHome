sudo apt update

sudo apt install libgdiplus mysql-server nginx -y

sudo service mysql start
sudo service nginx start

nginxConfig="
server {
    listen        80;

    location / {
        proxy_pass         http://localhost:5000/;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade \$http_upgrade;
        proxy_set_header   Connection \$http_connection;
        proxy_set_header   Host \$host;
        proxy_cache_bypass \$http_upgrade;
        proxy_set_header   X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto \$scheme;
    }

    location ~* /admin {
        proxy_pass         http://localhost:5001;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade \$http_upgrade;
        proxy_set_header   Connection \$http_connection;
        proxy_set_header   Host \$host;
        proxy_cache_bypass \$http_upgrade;
        proxy_set_header   X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto \$scheme;
    }
}
"

sudo echo "$nginxConfig" > /etc/nginx/sites-enabled/default
sudo nginx -s reload

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update; \
	sudo apt-get install -y apt-transport-https && \
	sudo apt-get update && \
	sudo apt-get install -y dotnet-sdk-3.1

dotnet tool install -g Microsoft.Web.LibraryManager.Cli

cd ../CoreHome.HomePage
dotnet restore
~/.dotnet/tools/libman restore
dotnet build
dotnet publish -o ../bin/CoreHome/HomePage

cd ../CoreHome.Admin
dotnet restore
~/.dotnet/tools/libman restore
dotnet build
dotnet publish -o ../bin/CoreHome/Admin