FROM petrjahoda/mariadb-nanoserver
EXPOSE 3306
ADD /config /mariadb
ADD /database /mariadb/bin
SHELL ["pwsh", "-Command"]
USER Administrator
RUN \
    c:\mariadb\bin\mysqld \
        --install MariaDB
RUN \
    Start-Service \
        -Name MariaDB \
        -Verbose ;
WORKDIR "c:\mariadb\bin"
RUN \
    .\mysql.exe \
        -u root\
        -p54321\
    -e 'CREATE OR REPLACE DATABASE new_database;'
RUN \
    .\mysql.exe \
       -u root new_database\
       -p54321\
    -e 'source install_db.sql;'
RUN \
    .\mysql.exe \
       -u root mysql\
       -p54321\
    -e 'source update_root_pwd.sql'
RUN \
    .\mysql.exe \
       -u root mysql\
    -p54321\
    -e 'FLUSH PRIVILEGES'
ENTRYPOINT c:\Windows\System32\ping.exe -t localhost
