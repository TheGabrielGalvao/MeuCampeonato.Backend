# Meu Campeonato (Backend)

Este é o backend do projeto "Meu Campeonato". O backend é desenvolvido em C# utilizando ASP.NET Core e implementa as funcionalidades necessárias para simular campeonatos de futebol de times cadastrados pelo usuário.

## Funcionalidades
- Cadastro de times: O usuário pode cadastrar seus times para usá-los em suas simulações
- Simulação: O usuário pode selecionar os times que deseja para fazer a simulação. É necessário selecionar 8 times para que a simulação seja gerada, pois as competições sempre  são mata-mata e começam na faze de quartas de final
- Histórico: Nessa tela é possível acessar as simulações geradas pelo usuário e ver os detalhes de cada uma delas
 

## Tecnologias Utilizadas

- [ASP.NET Core](https://dotnet.microsoft.com/download) - Framework web utilizado para construir o backend.
- [BCrypt](https://github.com/BcryptNet/bcrypt.net) - Biblioteca para hash de senhas.
- [Dapper](https://dapper-tutorial.net/) - ORM (Object-Relational Mapping) para interagir com o banco de dados.
- [DbUp](https://dbup.readthedocs.io/) - Ferramenta para versionamento e execução de scripts SQL.
- [Swagger](https://swagger.io) - Ferramenta para realizar testes da API em ambiente de desenvolvimento.
- [NUnit](https://nunit.org) - Ferramenta para auxiliar na implementação de testes automatizados.
- Autenticação JWT - Para autenticação e autorização de usuários.
- SQL Server - Banco de dados relacional utilizado para armazenar informações.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes requisitos instalados:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) (opcional, dependendo das suas preferências)
- SQL Server

## Configuração

1. Clone o repositório:

   ```bash
   git clone https://github.com/TheGabrielGalvao/MeuCampeonato.Backend.git

2. Abra o projeto no seu ambiente de desenvolvimento

3. Configure a string de conexão do banco de dados no arquivo appsettings.json:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "SuaConnectionStringAqui"
      },
      "JwtSettings": {
        "SecretKey": "SuaChaveSecretaAqui",
        "Issuer": "SeuIssuerAqui",
        "Audience": "SuaAudienceAqui"
      }
    }

## Arquitetura

    A aplicação foi desenvolvida utilizando um modelo de arquitetura em camadas, que consiste em separar
    responsabiliades de forma que cada camada funciona como uma peça para o funcionamento do todo. 
    As camadas da aplicação são as seguintes:
- #### API:
    Essa camada é um projeto do tipo API. Ela é a camada principal da aplicação, pois é o projeto setado como
    startup. Nela temos nossos arquivos de configuração da aplicação como appsettings.json, que tem algumas
    chaves que são usadas na aplicação por meio de injeção de dependência e também o lauchsettings que é um
    arquivo de configuração de ambiente do Visual Studio que possibilita a criação de perfis de execução para
    a nossa aplicação. Possui também o arquivo Program.cs, que é o arquivo que é executado quando damos o
    start da nossa aplicação. Nesse arquivo é onde são feitas as definições de como a aplicação deve funcionar
    como normas e injeções de dependências necessárias para o funcionamento correto.
    Possui também uma pasta Controllers, que é onde ficam as classes que representam os endpoints da
    aplicação, as rotas que são executadas nas chamadas para o backend.
- #### Database:
    Essa camada é um projeto do tipo biblioteca de classes e é responsável por agrupar todas as classes
    responsáveis pelas configurações de conexão com o banco de dados. Nessa aplicação temos apenas 2 classes
    que são necessárias para o funcionamento:
    AppDbContext: Responsável por instanciar a conexão com o banco de dados e disponibilizar esa instância
    para ser usada quando necessário
    DatabaseSetup: Classe responsável pela configuração da ferramenta DbUp, que é a ferramenta usada para
    fazer as atualizações do banco de dados. As atualizações são feitas com base nos arquivos presentes nas
    pastas Schema, Table e View. Essas pastas contém arquivos SQL de criação de Schemas, Tabelas e Views
    respectivamente. Os arquivos devem estar com a propriedade "Build Action" como "Embeded Resource" para que
    o DbUp consiga usá-los nas suas atualizações.
- #### Domain:
    Essa camada é um projeto do tipo biblioteca de classes e é responsável por agrupar todas as classes
    responsáveis por definir a tipagem da aplicação. Ela possui as seguintes subpastas:
    DTO: Essa pasta contém as DTOs, ou seja, as classes que são usadas como modelo para requisição e retorno
    de dados da nossa aplicação.
    Entity: Essa pasta contém as entidades da aplicação
    Enum: Essa pasta contem os Enumerators usados especificamente nas entidades e DTOs da aplicação
    Interface: Essa pasta contém as interfaces(Contratos) da aplicação, ou seja, são um modelo de como classes
    de regra de negócio devem funcionar baseada em herança. Todos os métodos das interfaces devem ser
    implementados nas classes que as herdam. É dividida em 2 subpastas: Repository e Service que são também
    nomes de camadas da nossa aplicação que serão explicadas abaixo
- #### Mapper:
    Essa camada é um projeto do tipo biblioteca de classes e é responsável por agrupar todas as configurações
    de mapeamento e injeção de dependência da aplicação. Aqui é definido qual interface representa qual classe
    concreta e também como deve se comportar os mapeamentos de uma entidade para uma DTO por exemplo
- #### Repository:
    Essa camada é um projeto do tipo biblioteca de classes e é responsável por agrupar todas as classes
    responsáveis por fazer requisições no banco de dados da aplicação
- #### Service:
    Essa camada é um projeto do tipo biblioteca de classes e é responsável por agrupar todas as classes
    responsáveis pelas regras de negócio da aplicação
- #### Tests:
    Essa camada é um projeto do tipo biblioteca de classes e como o nome já diz, é responsável por agrupar
    todas as classes de teste da alpicação
- #### Util:
    Essa camada é um projeto do tipo biblioteca de classes e é responsável por agrupar todas as classes que
    são utilitárias para a aplicação como um todo e não especificamente de um único contexto
  
