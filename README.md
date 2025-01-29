---------------------------------------------------------------------------------------------------------------------------------------------------------------
################################################ ROTAVIAGEM - Sistema de Roteamento de Viagens ################################################################
---------------------------------------------------------------------------------------------------------------------------------------------------------------
Este sistema permite registrar rotas de viagem, calcular a melhor rota entre dois pontos e persistir as informa��es de rotas no banco de dados.
O objetivo � encontrar a rota mais barata entre dois destinos, com base em rotas previamente cadastradas.
---------------------------------------------------------------------------------------------------------------------------------------------------------------
################################################# REQUISITOS: #################################################################################################
---------------------------------------------------------------------------------------------------------------------------------------------------------------

1. .NET 6 ou superior
2. Banco de dados SQL Server (ou outro banco configurado no contexto)

---------------------------------------------------------------------------------------------------------------------------------------------------------------
################################################ Passos para Execu��o: ########################################################################################
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	1. Clonar o reposit�rio:
	-----------------------------------------------------------------------------------------------------------------------------------------------------------

		git clone <url-do-reposit�rio>
		cd RotaViagem

	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	2. Restaurar as depend�ncias:
	-----------------------------------------------------------------------------------------------------------------------------------------------------------

		dotnet restore

	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	3. Configurar a string de conex�o no arquivo appsettings.json com o banco de dados desejado:
	-----------------------------------------------------------------------------------------------------------------------------------------------------------

		{
		  "ConnectionStrings": {
			"RotaViagemDb": "Server=localhost;Database=RotaViagemDb;Trusted_Connection=True;"
		  }
		}

	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	1. 4. Aplicar as migra��es do banco de dados:
	-----------------------------------------------------------------------------------------------------------------------------------------------------------

		dotnet ef database update

	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	5. Executar a aplica��o:
	-----------------------------------------------------------------------------------------------------------------------------------------------------------

		dotnet run

	-----------------------------------------------------------------------------------------------------------------------------------------------------------
	6. A API estar� dispon�vel em http://localhost:5000 (ou a URL configurada).	
	-----------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------
######################################## Estrutura dos Arquivos/Pacotes #######################################################################################
---------------------------------------------------------------------------------------------------------------------------------------------------------------

A aplica��o segue uma estrutura baseada em m�ltiplos projetos e est� organizada da seguinte forma:

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Api
---------------------------------------------------------------------------------------------------------------------------------------------------------------
Cont�m os Controllers da API e os endpoints para interagir com a aplica��o.

* RotaController.cs: Respons�vel por expor os endpoints para cadastro de rotas e consulta da melhor rota.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Application
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Cont�m a l�gica de neg�cios e servi�os respons�veis pelo processamento dos dados.

* RotaService.cs: Servi�o que cont�m a l�gica para adicionar rotas e calcular a melhor rota.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Domain
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Cont�m as entidades e interfaces de reposit�rios.

* Rota.cs: Entidade que representa uma rota, com propriedades como Origem, Destino e Valor.
* IRotaRepository.cs: Interface do reposit�rio que define os m�todos para manipular rotas.
---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Infrastructure
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Cont�m as implementa��es de persist�ncia de dados.

* RotaRepository.cs: Implementa��o do reposit�rio que acessa o banco de dados usando Entity Framework Core.
* RotaViagemContext.cs: Contexto do banco de dados utilizando o Entity Framework Core para mapear as entidades para tabelas do banco.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
RotaViagem.Tests
---------------------------------------------------------------------------------------------------------------------------------------------------------------

Cont�m os testes unit�rios e de integra��o para garantir o funcionamento correto da aplica��o.

* RotaServiceTests.cs: Testes para validar o comportamento do RotaService.



---------------------------------------------------------------------------------------------------------------------------------------------------------------
#################################################### DECIS�ES DE DESIGNER #####################################################################################
---------------------------------------------------------------------------------------------------------------------------------------------------------------

A solu��o foi desenvolvida com base em boas pr�ticas de arquitetura de software, buscando clareza, escalabilidade e flexibilidade. Algumas das principais
decis�es de design s�o:

---------------------------------------------------------------------------------------------------------------------------------------------------------------
1. Arquitetura em Camadas:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Utilizamos uma arquitetura em camadas para separar a l�gica de neg�cios, acesso a dados e a exposi��o da API.
	* A camada de API serve como interface com o cliente, a Application cont�m os casos de uso e l�gica de neg�cios, 
	  a Domain define as entidades e regras de neg�cio, e a Infrastructure realiza a persist�ncia de dados.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
2. DDD (Domain-Driven Design):
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* A aplica��o segue os princ�pios do DDD para separar a l�gica de neg�cios das preocupa��es de infraestrutura e UI.
	  As entidades como Rota s�o colocadas no dom�nio, e os reposit�rios s�o definidos como interfaces na camada de dom�nio.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
3. Uso de Entity Framework Core (EF Core):
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* O EF Core foi utilizado para gerenciar a persist�ncia de dados. Ele permite mapear as entidades para o banco de dados de forma simples e eficiente.
	* O uso de transa��es n�o foi explicitamente implementado, mas a l�gica de persist�ncia no reposit�rio garante que a integridade dos dados seja mantida.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
4. C�lculo da Melhor Rota:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Para calcular a melhor rota entre dois pontos, foi utilizada uma abordagem recursiva simples, que explora todas as rotas poss�veis entre a origem
      e o destino.
	* O custo de cada rota � calculado somando o valor das rotas intermedi�rias. A melhor rota � selecionada com base no custo total.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
5. Testes Unit�rios:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Utilizamos o framework XUnit para testes unit�rios, com o aux�lio do Moq para simular depend�ncias.
	* Os testes garantem que a l�gica de neg�cios no RotaService esteja funcionando conforme o esperado.

---------------------------------------------------------------------------------------------------------------------------------------------------------------
6. Persist�ncia de Dados:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* Utilizamos o Entity Framework Core para persistir as rotas no banco de dados, com a abordagem de reposit�rios para abstrair a camada de persist�ncia.
	* O reposit�rio implementa o padr�o de reposit�rio, permitindo f�cil substitui��o por outra implementa��o no futuro (como um reposit�rio em mem�ria ou
	  baseado em outro banco de dados).

---------------------------------------------------------------------------------------------------------------------------------------------------------------
7. Boas Pr�ticas de C�digo:
---------------------------------------------------------------------------------------------------------------------------------------------------------------

	* A aplica��o segue os princ�pios SOLID, garantindo que o c�digo seja modular, reutiliz�vel e de f�cil manuten��o.
	* A l�gica de neg�cios foi extra�da para a camada de aplica��o, mantendo os controllers da API focados apenas na manipula��o das requisi��es HTTP.