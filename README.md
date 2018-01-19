# Gamemanager

Aplicação feita em Asp.Net Core, Entity Framework, Identity, Dependency Injection

Uso das tecnologias e ferramentas
- jQuery: Framework JavaScript utilizada nesse projeto para requisições Ajax na tela de jogos;
- nUnit: Utilizado para testes diversos, como unitários, integração, regressão, etc (no projeto foram realizados testes de integração da classe GameService);
- Moq: Framework para facilitar testes independente de classes filhas, além de classes injetadas automaticamente;
- ORM: Mapeamento objeto-relacional, facilita a manipulação de registro no banco de dados, nessa aplicação foi utilizado o Entity Framework Core;
- Identity: Framework da Microsoft utilizada para autenticação e autorização. O contexto de banco foi unificado ao da aplicação, mas os managers e dominios ficaram separados;
- Dependency Injection: Injeção de dependencia com o registro das dependencias em projeto separado, minimizando a dependencia entre projetos;
- Asp.Net Core: Nova plataforma da Microsoft multiplataforma;
- DDD: Desenvolvimento dirigido ao domínio, onde temos nosso dominio limpo, classes simples (POCO), representando as entidades utilizadas na aplicação;
- MVC: Padrão de arquitetura mais amplamente utilizado para aplicações Web;
- Programação assincrona: Utilizada no envio de e-mails de cobrança de jogos atrasados;
- RestSharp: Framework para executar requests rests a apis de maneira simples;
- MailGun: WebApi para envio de e-mails;

As tecnologias e arquitetura utilizada nesse projeto não condizem com o porte da aplicação, mas serve para demonstrar uma parte dos recursos disponíveis atualmente para desenvolvimento de aplicações web.

Pontos a serem melhorados
- Edição de empréstimos;
- Visualização de histórico de empréstimos;
- Utilização do AutoMapper, eliminando os métodos de conversão de dados (pode-se também criar alguma estrutura de mapper própria);
- Implementação dos testes restantes (o correto seria ter realizado com TDD todas as classes, mas pelo quesito tempo e a não necessidade de manutenção posterior não foi utilizado essa metodologia de desenvolvimento);