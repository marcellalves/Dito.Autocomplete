# 1 - Serviço de Autocomplete

Para o problema proposto, considerando as condições de escalabilidade e tempo de resposta, optei pela utilização de tecnologias distribuídas rodando dentro de containers do Docker:

- Api Coletora: .Net Core 3.1, 
- Banco de dados local: Cluster de bancos MongoDB
- Busca: Elasticsearch
- Sincronização dos dados do armazenamento local para o Elasticsearch: monstache ([https://rwynn.github.io/monstache-site/](https://rwynn.github.io/monstache-site/))

# Arquitetura da solução

## Inclusão de eventos de usuário
![Desenho arquitetural da consulta de autocomplete](https://drive.google.com/uc?export=view&id=1hQgOgTHaYMwR1n9w0WOkSZCHEeWrkPUY)

## Pesquisa de termo para autocomplete
![Desenho arquitetural da inclusão de eventos](https://drive.google.com/uc?export=view&id=10Zif_-u9k2HtMLK1lLt5ISKYxrRj3BRb)

# Executando a aplicação

### 1 - Rodar os containers do projeto com um dos seguintes scripts:

	- startup.sh (para rodar com bash)

	- startup.ps1 (para rodar com PowerShell)

### 2 - Rodar o script de criação do índice e do mapeamento no Elasticsearch:

	- es_index_setup.sh (para rodar com bash)

	- es_index_setup.ps1 (para rodar com PowerShell)

###  3 - Criar o cluster de MongoDB para habilitar a sincronização do monstache:

#### 3.1 - Conectar no container do MongoDB:

	docker exec -it mongo0 mongo --port 30000

  #### 3.2 - Dentro do shell do container do MongoDB, executar:

	config={"_id":"rs0","members":[{"_id":0,"host":"mongo0:30000"},{"_id":1,"host":"mongo1:30001"},{"_id":2,"host":"mongo2:30002"}]}

	rs.initiate(config);

### 4 - Inserir eventos de navegação dos usuários através do swagger, na URL: https://localhost:32796/swagger/index.html ou através da coleção do postman presente no diretório postman/Dito.Autocomplete.postman_collection.json

### 5 - Testar a funcionalidade de autocomplete através do swagger (get/autocomplete) ou utilizando a coleção do postman (Teste autocomplete)

### 6 - Para rodar os testes unitários, basta executar o comando abaixo na pasta raíz da aplicação:

	dotnet test Dito.Autocomplete.UnitTests/Dito.Autocomplete.UnitTests.csproj
