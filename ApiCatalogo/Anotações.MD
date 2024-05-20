#Anotações

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
	entidades para acompanhar o estado das entidades. Esse Recurso adiciona uma sobrecrga que afeta o desenpenho das consultas rastreadas

	Para aumentar o desenpenho em consultas de leitura usamos o metodo AsNoTracking que tira essa sobrecarga nesse tipo de consulta.

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
O model bindig é um recirso que nos permite mapear dados de uma requisição http para os parâmetro de uma action de um controlador
Esse mapeamento inclui todos os tipos de parâmetros: numeros, string, array, etc

Semrpe que uma requis~ção é recebida o framework asp .net core precisa tratar essa requisição de forma que possa passar valores 
apropriados nos parâmetros das actions.

Como funciona: Quando nossa api rece uma requisição HTTP ele roteia para um metodo action especifico de um controlador com base na rota definida
Ela termina qual o metodo actions será executado com base no que estpa definido na rota e em seguida vincula os valores da requisição HTTP aos 
parâmetros desse método action.

Fontes de dados 
1-Valores de formulario 
2-Rotas 
3-Query Strings

