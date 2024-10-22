#Anotações

API - Application Programming Interface :
É o conjunto de rotinas e pad~ros estabelecidos e documentados por uma aplicação para que outras aplicações consigam utilizar suas 
funcionanlidades sem precisar conhecer detalhes da sua implmentação.

Web Services :
São um conjunto de métodos invocados por outras aplicações usando tecnologias qwb sendo uma solução utilizada na integração de sistemas 
e na comunicação entre aplicações distintas.

Web Api:
É uma api na internet, com um conjunto de serviços expostos via web para realizar a integração da aplicação com diversos tipos de clientes.


- Conceitos

	Soap - Simple Object Access Protocol: É um protocolo baseado em XML, usado para trocar informações entre aplicações no mesmo ou em diferentes sistemas operacionais
			
	REST - Representational State Transfer Protocol : É um estilo arquitetural sem estado que consiste de um conjunto coordenado de restrições arquiteturais 
		   aplicadas a componentes,conectores e elementos de dados dentro de um sistema de hipermídia distribuido.

AppDbContext: Classe que implementa o DbContext que representa uma sessão com o banco de dados que pode ser usada
			  para consulta e salvar instancias das nossas entidades no banco de dados.

			  No construtor usamos a classe dbcontextOption pra configurar o contexto do efCore na classe base, definindo 
			  o provedor do banco de dados e a string de conexão.
DBContext: Representa uma sesão com o banco de dados sendo a ponte entre as entidades de dominio e o banco de dados
DbSet<T>: Representa uma coleção de entidades no contexto que podem ser consultadas no banco de dados

Data Annotations
São conjuntos de atributos que podem ser aplicados a classes e seus membros para fornecer metadados sobre como esses 
recursos devem ser tratados pelo sistema;

-Realizar validações de entrada de dados 
-Influenciar o comportamento do modelo de dados

Controllers:

São arquivos onde estão os métodos que são uteis para lidar com as requisições http

[APIController]:
Atributo que premite decorar os controladores habilitando recursoso como:
-Requisito de roteamento de atributo;
-Resposta HTTP 400 automaticas
-Inferencia de parâmetro de origem de associação
-Inferencia de solicitação de dados de varias partes
-Uso de problem Details para codigos de status de erro

[Route]:
Especifica um padrão de url para acessar um control


ReferenceHandler
Define como o JsonSerialxer lida com referencias sobre serialização e desserialização

Dicas de otimização
1- Quando consultamos entidades usando entity framework ele armazena as entidades no contexto (em cache) realizando o tracking das 
	entidades para acompanhar o estado das entidades. Esse Recurso adiciona uma sobrecrga que afeta o desempenho das consultas rastreadas

	Para aumentar o desempenho em consultas de leitura usamos o metodo AsNoTracking que tira essa sobrecarga nesse tipo de consulta.

2- Nunca retorne todos os registros em um consulta
	sempre aplique um filtro e reduza o numero de objetos no retorno, pode usar o metodo take.

Tratemento de erros
Para configurar uma pagina de tratamento de erros personalizada para o ambiente de produção usamos o middleware UseExceptionHandler

Captura e registra exceções não tratadas
executa novamente o request em um pipe line alternativo usando o caminho indicado. O codigo gerado executa o request usando o caminho /Error

Roteamento 
É o responsavel por receber as requisições http e encamihar para os endpoints da controller representados pelos metodos action
- Mapeia as requisições feitas via URI
- Despacha as requisições para os endpoint
- Pode extraiar valores da URL da requisição
- Pode gerar URLs que mapeiam para os endpoints

O roteamento nas WebAPIs é baseado em atributos
A rota é determinada com base nos atributos definidos nos controladores e métodos Action
As definições nos atributos irão realizar o mapeamento para as actions do controlador
Podemos definir rotas nos atributos HttpGet, HttpPost, HttpPut e  HttpDelete que irão comport a rota definida no atributo Route

Habilitando explicitamente o roteamento 

Usando o UseRouting e UseEndpoints
UseRouting:Adiciona correspondencia de rota ao pipeline de middleware. Esse middleware analisa o conjunto de endpoints definidos 
no aplicativo e seleciona a melhor correspondencia com base na requisição.

UseEndpoints:Adiciona a execução de eno=point ao pipeline de middleware. Ele executa o delegate associado ao end point selecionado.

Restrições de rotas:
Nos ajudam a filtrar ou impedir que os valores indesejadps atinjam os metodos action do controlador
isso é feito verificando a restrição com relação ao valor do parâmetro da URL

Tipos de retorno 
Tipo especifico:
retorna um tipo de dados complexo ou primitivo 

IActionResult:
É apropriado quando vários tipos de retorno actionREsult são possivel em métodos action

ActionResult:
Permite o retorno de um tipo especifico derivado de actionResult ou o retorno de um tipo especifico

Metodos action 
O servidor de uma aplicação .net possui um pool de threads que são usadas para servir as requisições que 
chegam em um método action.

- Sincronos:
Quando um request chega, uma threads desse pool é encarregada de processar esse request.

Se o request for proceesado de forma sincrona, a thread encarregada ficará bloqueada até o request finalizar.

Assim, execução dos métodos é continua e se uam thread começar a executar um método, ela irá fiar ocupada até 
a execução do método terminar.

- Assíncrono
Quando um request chega uma thread desse o=poool é encarregada de processar esse request

Se o request for processado de forma assincrona, a thread é devolvida ao pool para servir novos requests.

Assim, a execução dos métodos não é continua.
Quando a operação assincrona terminar, a thread é avisada e retoma o controle de execução da action.

Programação assincrona em .net
No .net a abordagem é fazendo o uso das palavra async e await para criar metodos assincronos.
É uma boa pratica definir o nome do metodo com Async no final quando ele for um metodo assincrono.
Regras da programação assincrona no .net
1- A assinatura do metodo dee incluir o medificador async;
2- O método deve ter u tipo de retorno da task<Tresult>, taskou void;
3- As declarações de método devem incluir pelo menos uma única expressão await- isso diz ao compilador que o métdo 
precisa ser suspenso enquanto a operação agurdada estiver ocupada.
4- Por ultimo, o nome do método dee terminar como sufixo "Async"

Models Binding
O model bindig é um recurso que nos permite mapear dados de uma requisição http para os parâmetro de uma action de um controlador
Esse mapeamento inclui todos os tipos de parâmetros: numeros, string, array, etc

Sempre que uma requisição é recebida o framework asp.net core precisa tratar essa requisição de forma que possa passar valores 
apropriados nos parâmetros das actions.

Como funciona: Quando nossa api recebe uma requisição HTTP ele roteia para um metodo action especifico de um controlador com base na rota definida
Ela termina qual o metodo actions será executado com base no que está definido na rota e em seguida vincula os valores da requisição HTTP aos 
parâmetros desse método action.

Fontes de dados 
1-Valores de formulario 
2-Rotas 
3-Query Strings

[bind] - Obtém propriedades específicas

FromServices:
Sobrescreve a fonte de associação injetando os valores via injeção de dependencia em um método Action específico.

Permite injetar as dependências diretamente no método Action do controlador que requer a dependência

Habilita a injeção de um serviço diretamente em um método Action sem usar a injeção de construtor

A partir do .net 7 a implementação do fromservices é feita de forma implicita, ou seja, não é necessario usar o atributo no método Action

É possivel desabilitar o padrão dessa forma dentro do arquivo program:

builder.services.configure<ApiBehaviorOptions>(options=>
{
	options.DisableImplicitFromServicesParameters=true;
});

DataAnnotations:
Esse namespace fornece classes e atributos que são usado spara realizar a validação dos dados.

Os atributos Data Annotations permitem aplicar a validação no modelo de domínio definindo os atributos.

Validação personalizada
 como criar uma validação persolizada
 1- Criar atributos personalizados
	- seu foco é validar uma propriedade
	- Pode ser reutilizada em diversos modelos
 2 - Implementar IValidatableObject no seu modelo
	-Pode acessar todas as propriedades do modelo e realizar uma validação mais complexa
	- Não pode ser reutilizada em outros modelos.

	1- Criar atributos
		- Criar uma classe que herda de validationAttribute e sobreescrever o metodo IsValid
		O método isvalid aceita dois parametros:
			-um objeto value, que é a entrada a ser validada
			-um objeto validaionContext, que fornece informações adicionais, como a instancia de modelo criada por model binding;
			
			O método isValid retorna a classe ValidationResult, através de seu campo estático Sucess ou iniciailizando uma nova instancia(no caso de um erro)


Modelo de configuração no .NET

O arquivo de configuração é o appsettings.json

A configuração de aplicativos asp.net core se baseia em uma lista de pares chave-valor estabeliddos por provedores de configuração.

Os provedores de configuração leem os dados de configuração em pares chave-valoe de várias fontes de configuração.
Em aplicações .net esse aruivo de configuração eé o appsettings.json.

OBS: A variavel aspnetcore_environment determina o ambiente em a aplição está sendo executada, com isso podemos criar
um appsettings.json para cada ambiente, esta variavel deve ficar no arquivo launchSettings.json.  

OBS:Builder representa uma instancia de WebApplicationBuilder e possui uma propriedade configuration que é 
uma instância de Iconfiguration, fazendo com que não seja necessaria a injeção de dependencia para realizar a leitura dos aqquivos de configuração.

Middleware
	- Um middleware é um trecho de codigo dentro do pipe line de requisições, ou seja uma cadeia de processamentos por onde a requisição passa,onde é 
	processada e vai para o proximo até ser processada pela aplicação.

	Sua ordem importa, pois o fluxo de execução pode ser interrompido, caso uma condição não eja atendida.

	Os middlewares são executados na classe program.

	Request Delegate:  É uma função que recebe um objeto de contexto do request HTTP e exxecuta uma lógica específica para esse request.

Tratamento global de exceções 

Podemos usar o middleware UseExceptionHandler para realizar o tratamento de exceções

Ele pode ser usado para manipular exceções globalmente e obter todos os detalhes do objeto Exception

Como fazer?
Vamos configurar o middleware de tratamento de exceções UseExceptionHandler para capturar exceções não tratadas, definir uma resposta http com um codigo
de status 500, obter os detalhes da exceção no formato json e enviar a resposta de volta ao cliente.

Roteiro para usar o midleware
1- Criar uma entidade chamada errodatails que é uma classe usada para representar os detalhes dos erros
2- Criar um método de extensão configureExceptionHandler para IApplicationBuilder
3-usar o middleware UseExceptionHandler e configurar o tratamento de erros
4-Habilitar o uso do método de extensão ConfigureExceptionHandler na classe Program(app.configureExceptionHandler())
5-Testar a implementação no swagger e no postman
