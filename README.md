# Desafio-Back-Boticario

Teste prático backend Boticário.

http://localhost:64732/swagger/index.html

É necessario alterar a string de conexão do projeto no aquivo appsettings.json no projeto Cashbot.Services.Api, após alterar a string de conexão é só rodar o comando

Update-Database -StartupProject Cashbot.Services.Api -Project Cashbot.Infra.Data

para criar a base.

Foram feito os testes do Dominio e Application.

É necessario também se autenticar para consumir as Apis, para isso tem um EndPoint login onde é preciso logar com um revendedor default

e-mail: master@grupoboticario.com.br
senha: teste@123

após isso o sistema irá gerar um token ai é só adicionar o token gerado no swagger como o exemplo abaixo


 Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Im1hc3RlckBncnVwb2JvdGljYXJpby5jb20uYnIiLCJqdGkiOiJkZmM1M
