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
