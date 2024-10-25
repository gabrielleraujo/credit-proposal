#!/bin/bash

# Listar os arquivos e diretórios em /app (opcional, para depuração)
echo "Conteúdo do diretório /app:"
ls /app
#ls /app/CreditProposal.API

# Executar a aplicação
dotnet /app/CreditProposal.API.dll
