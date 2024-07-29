# Desafio Sistema de Gerenciamento de Tarefas

**Primeiro passo:** baixa o projeto na máquina e executa a linha de comando abaixo:<br>
**git clone https://github.com/alexsoliveira/Desafio.SistemaGerenciamentoTarefas.git**

**Segundo Passo:** quando finalizar de baixar o projeto, executa a linha de comando abaixo:<br>
**cd Desafio.SistemaGerenciamentoTarefas/**

**Terceiro passo:** dentro da pasta do projeto **"/Desafio.SistemaGerenciamentoTarefas"** <br>
executar o comando abaixo:<br>
**docker compose up -d**

**Quarto passo:** a finaizar de subir os containers, executa os comandos abaixo para subir o banco e a tabela "tarefa":<br>
**$Env:ASPNETCORE_ENVIRONMENT = "Migrations"** <br>
**dotnet ef database update -s ./src/Desafio.SisGerTarefas.Api/ -p ./src/Desafio.SisGerTarefas.Infra.Data.EF/ -v**

**Quinto passo:** executa os comandos abaixo para subir a tabela "identity":<br>
**cd .\src\Desafio.Identidade.Api** <br>
**$Env:ASPNETCORE_ENVIRONMENT = "Migrations"** <br>
**dotnet ef database update -v**

**Sexto passo:** executa as duas urls no browser:<br>
**http://localhost:5000/swagger/index.html** <br>
**http://localhost:5002/swagger/index.html**

**Sétimo passo:** para testar a aplicação, cadastra um novo usuário, dentro do swagger<br>
**http://localhost:5000/swagger/index.html** clica na api **/api/identidade/nova-conta** e executa  json abaixo:<br>
``{
  "email": "teste@testes.com",
  "senha": "Teste@123",
  "senhaConfirmacao": "Teste@123"
}``

**Oitavo passo:** para testar logar na aplicação, depois que executou json acima,<br>
executa dentro do swagger **http://localhost:5000/swagger/index.html** clica na api<br>
**/api/identidade/autenticar** e executa json abaixo:<br>
``{
  "email": "teste@testes.com",
  "senha": "Teste@123"
}``

**Nono passo:** para testar criar tarefa, cópia o accessToken e o seu id dentro do resutado da execução anterior,<br>
em seguida dentro do swagger **http://localhost:5002/swagger/index.html** clica n botão **Authorize** <br>
na janela que abre digita **Bearer {cola seu accessToken aqui}** e em seguida executa o json abaixo:<br>
``{
  "idUsuario": "{cola seu id do usuário aqui}",
  "titulo": "Titulo Tarefa 1",
  "descricao": "Descricao Tarefa 1"
}``

**Décimo passo:** rodar os testes de unidade, integração e end to end, executa o comando abaixo:<br>
**cd ..\..\tests\Desafio.SisGerTarefas.UnitTests** <br>
**dotnet test** <br>
**cd ..\Desafio.SisGerTarefas.IntegrationTests** <br>
**dotnet test** <br>
**cd ..\Desafio.SisGerTarefas.EndTEndTests** <br>
**docker compose up -d** <br>
**dotnet test** <br>





