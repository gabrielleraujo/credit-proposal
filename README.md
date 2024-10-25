# Customer registration

### New Migration:
- para criar uma migração do banco de dados execute o comando abaixono terminal no caminho "src/CreditProposal.Infrastructure".1. dotnet ef --startup-project ../CreditProposal.API/  migrations add initial -c CreditProposalContext
2. dotnet ef --startup-project ../CreditProposal.API/  database update

### Docker Compose:
A execuçao do script depende do container do rabbitmq ter sido inicializado para poder subir a api, o rabbitmq está no docker-compose do ms de customer_registration, o quel é o primeiro ms que deve ser inicializado.

### Executar scripts no VS Code:
Terminal > Run Task... > docker-run

### Debub no VS Code:
Run and Debug > .NET Core Docker Attach

### Abrir banco de dados no Azure Data Studio:
utilizar a connction string mudando o o server para localhost e especificar a porta configurada no docker-compose.
Ex: Server=localhost,1434

### Swagger:
- https://localhost:7014/swagger/index.html

#### Some usage examples:

<p align="justify"><img width="700" src="./assets/images/swagger.png"/></p>
<p align="justify"><img width="700" src="./assets/images/middleware_error_response.png"/></p>
<p align="justify"><img width="700" src="./assets/images/post_credit_proposal.png"/></p>
<p align="justify"><img width="700" src="./assets/images/get_credit_proposal.png"/></p>


