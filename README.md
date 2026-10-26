# MedicaMap

Plataforma web para centralização e consulta de dados de estoque de medicamentos no Sistema Único de Saúde (SUS).

## Sobre o projeto

O MedicaMap é uma aplicação web desenvolvida com o objetivo de facilitar a consulta e a organização de informações públicas relacionadas aos estoques de medicamentos e aos estabelecimentos de saúde.

A plataforma utiliza dados relacionados à Assistência Farmacêutica, incluindo informações provenientes da Base Nacional de Dados de Ações e Serviços da Assistência Farmacêutica (BNAFAR). A proposta é apresentar essas informações de maneira estruturada e acessível, facilitando a consulta por usuários interessados na disponibilidade de medicamentos na rede pública de saúde.

O projeto foi desenvolvido como parte do Trabalho de Conclusão de Curso de Bacharelado em Engenharia de Software.

## Funcionalidades

- Consulta de medicamentos cadastrados.
- Pesquisa de medicamentos por nome.
- Consulta de estabelecimentos de saúde.
- Consulta de informações relacionadas aos estoques de medicamentos.
- Filtragem de estoques por localização e estabelecimento, conforme os parâmetros disponíveis.
- Importação de arquivos CSV para armazenamento local dos dados.
- Acompanhamento do progresso das importações.
- Consulta de informações organizadas em banco de dados relacional.

## Tecnologias utilizadas

### Front-end
- Angular
- TypeScript
- HTML
- CSS

### Back-end
- C#
- ASP.NET Core
- Entity Framework Core

### Banco de dados
- MySQL

### Fontes de dados
- Dados Abertos do Ministério da Saúde.
- Base Nacional de Dados de Ações e Serviços da Assistência Farmacêutica (BNAFAR).

## Arquitetura

A aplicação está organizada em três partes principais:

- **Front-end:** responsável pela interface com o usuário e pela apresentação das informações.
- **Back-end:** responsável pelas regras de negócio, pelo processamento das solicitações e pela integração com as fontes de dados.
- **Banco de dados:** responsável pelo armazenamento e gerenciamento das informações utilizadas pela aplicação.

A comunicação entre o front-end e o back-end ocorre por meio de requisições HTTP à API da aplicação.

## Pré-requisitos

Antes de executar o projeto, verifique se as seguintes ferramentas estão instaladas:

- Node.js v.22.
- Angular v.21.
- .NET SDK 8.0.
- MySQL Server.

## Como executar o projeto

As instruções abaixo são um roteiro inicial e devem ser ajustadas aos diretórios e arquivos reais do repositório.

### 1. Clonar o repositório

```bash
git clone https://github.com/Jackoki/MedicaMap.git
```

### 2. Executar o back-end

Acesse a pasta do projeto ASP.NET Core e execute:

```bash
dotnet restore
dotnet run
```

Configure a conexão com o MySQL no arquivo appsettings.json de acordo com o nome, senha e porta do serviço SQL.

### 3. Executar o front-end

Acesse a pasta do projeto Angular e execute:

```bash
npm install
ng serve
```

Depois, acesse o endereço local informado pelo Angular CLI no terminal.

## Fonte dos dados

Os dados públicos utilizados como referência para o projeto podem ser consultados no portal de Dados Abertos do Ministério da Saúde:

- [BNAFAR – Posição de Estoque](https://dadosabertos.saude.gov.br/dataset/bnafar-posicao-de-estoque)
- [API de Dados Abertos do Ministério da Saúde](https://apidadosabertos.saude.gov.br/v1/)

A disponibilidade e o formato dos dados dependem das fontes oficiais.

## Objetivo acadêmico

Este projeto foi desenvolvido como parte do Trabalho de Conclusão de Curso em Engenharia de Software, com foco na utilização de tecnologias web para facilitar o acesso e a consulta de informações públicas relacionadas à Assistência Farmacêutica no SUS.

## Autor

**Gabriel Kenji Inoue**

Bacharelado em Engenharia de Software  
Universidade Tecnológica Federal do Paraná (UTFPR) — Campus Cornélio Procópio.
