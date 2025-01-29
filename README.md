---------------------------------------------------------------------------------------------------------------------------------------------------------------
# ROTAVIAGEM - Sistema de Roteamento de Viagens 
---------------------------------------------------------------------------------------------------------------------------------------------------------------
Este sistema permite registrar rotas de viagem, calcular a melhor rota entre dois pontos e persistir as informações de rotas no banco de dados.
O objetivo é encontrar a rota mais barata entre dois destinos, com base em rotas previamente cadastradas.
---------------------------------------------------------------------------------------------------------------------------------------------------------------
# REQUISITOS: 
---------------------------------------------------------------------------------------------------------------------------------------------------------------

1. .NET 6 ou superior
2. Banco de dados SQL Server (ou outro banco configurado no contexto)

---------------------------------------------------------------------------------------------------------------------------------------------------------------
# Passos para Execução: 
---------------------------------------------------------------------------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------------------------------------------------------------------
1. Clonar o repositório:
-----------------------------------------------------------------------------------------------------------------------------------------------------------

	git clone <url-do-repositório>
	cd RotaViagem

-----------------------------------------------------------------------------------------------------------------------------------------------------------
2. Restaurar as dependências:
-----------------------------------------------------------------------------------------------------------------------------------------------------------

	dotnet restore

-----------------------------------------------------------------------------------------------------------------------------------------------------------
3. Configurar a string de conexão no arquivo appsettings.json com o banco de dados desejado:
-----------------------------------------------------------------------------------------------------------------------------------------------------------

	{
	  "ConnectionStrings": {
		"RotaViagemDb": "Server=localhost;Database=RotaViagemDb;Trusted_Connection=True;"
	  }
	}

-----------------------------------------------------------------------------------------------------------------------------------------------------------
1. 4. Aplicar as migrações do banco de dados:
-----------------------------------------------------------------------------------------------------------------------------------------------------------

	dotnet ef database update

-----------------------------------------------------------------------------------------------------------------------------------------------------------
5. Executar a aplicação:
-----------------------------------------------------------------------------------------------------------------------------------------------------------

	dotnet run

-----------------------------------------------------------------------------------------------------------------------------------------------------------
6. A API estará disponível em http://localhost:5000 (ou a URL configurada).	
-----------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------
# Estrutura dos Arquivos/Pacotes 
---------------------------------------------------------------------------------------------------------------------------------------------------------------

A aplicação segue uma estrutura baseada em múltiplos projetos e está organizada da seguinte forma:

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Api
---------------------------------------------------------------------------------------------------------------------------------------------------------------
Contém os Controllers da API e os endpoints para interagir com a aplicação.

* RotaController.cs: Responsável por expor os endpoints para cadastro de rotas e consulta da melhor rota.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Application
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Contém a lógica de negócios e serviços responsáveis pelo processamento dos dados.

* RotaService.cs: Serviço que contém a lógica para adicionar rotas e calcular a melhor rota.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Domain
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Contém as entidades e interfaces de repositórios.

* Rota.cs: Entidade que representa uma rota, com propriedades como Origem, Destino e Valor.
* IRotaRepository.cs: Interface do repositório que define os métodos para manipular rotas.
---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Infrastructure
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Contém as implementações de persistência de dados.

* RotaRepository.cs: Implementação do repositório que acessa o banco de dados usando Entity Framework Core.
* RotaViagemContext.cs: Contexto do banco de dados utilizando o Entity Framework Core para mapear as entidades para tabelas do banco.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Tests
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Contém os testes unitários e de integração para garantir o funcionamento correto da aplicação.

* RotaServiceTests.cs: Testes para validar o comportamento do RotaService.



---------------------------------------------------------------------------------------------------------------------------------------------------------------
# DECISÕES DE DESIGNER 
---------------------------------------------------------------------------------------------------------------------------------------------------------------

A solução foi desenvolvida com base em boas práticas de arquitetura de software, buscando clareza, escalabilidade e flexibilidade. Algumas das principais
decisões de design são:

---------------------------------------------------------------------------------------------------------------------------------------------------------------
1. Arquitetura em Camadas:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Utilizamos uma arquitetura em camadas para separar a lógica de negócios, acesso a dados e a exposição da API.
	* A camada de API serve como interface com o cliente, a Application contém os casos de uso e lógica de negócios, 
	  a Domain define as entidades e regras de negócio, e a Infrastructure realiza a persistência de dados.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
2. DDD (Domain-Driven Design):
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* A aplicação segue os princípios do DDD para separar a lógica de negócios das preocupações de infraestrutura e UI.
	  As entidades como Rota são colocadas no domínio, e os repositórios são definidos como interfaces na camada de domínio.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
3. Uso de Entity Framework Core (EF Core):
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* O EF Core foi utilizado para gerenciar a persistência de dados. Ele permite mapear as entidades para o banco de dados de forma simples e eficiente.
	* O uso de transações não foi explicitamente implementado, mas a lógica de persistência no repositório garante que a integridade dos dados seja mantida.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
4. Cálculo da Melhor Rota:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Para calcular a melhor rota entre dois pontos, foi utilizada uma abordagem recursiva simples, que explora todas as rotas possíveis entre a origem
      e o destino.
	* O custo de cada rota é calculado somando o valor das rotas intermediárias. A melhor rota é selecionada com base no custo total.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
5. Testes Unitários:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Utilizamos o framework XUnit para testes unitários, com o auxílio do Moq para simular dependências.
	* Os testes garantem que a lógica de negócios no RotaService esteja funcionando conforme o esperado.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
6. Persistência de Dados:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Utilizamos o Entity Framework Core para persistir as rotas no banco de dados, com a abordagem de repositórios para abstrair a camada de persistência.
	* O repositório implementa o padrão de repositório, permitindo fácil substituição por outra implementação no futuro (como um repositório em memória ou
	  baseado em outro banco de dados).

---------------------------------------------------------------------------------------------------------------------------------------------------------------
7. Boas Práticas de Código:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* A aplicação segue os princípios SOLID, garantindo que o código seja modular, reutilizável e de fácil manutenção.
	* A lógica de negócios foi extraída para a camada de aplicação, mantendo os controllers da API focados apenas na manipulação das requisições HTTP.
