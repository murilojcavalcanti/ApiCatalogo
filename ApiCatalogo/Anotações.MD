#Anotações

AppDbContext: Classe que implementa o DbContext que representa uma sessão com o banco de dados que pode ser usada
			  para consulta e salvar instancias das nossas entidades no banco de dados.

			  No construtor usamos a classe dbcontextOption pra configurar o contexto do efCore na classe base, definindo 
			  o provedor do banco de dados e a string de conexão.
DBContext: Representa uma sesão com o banco de dados sendo a ponte entre as entidades de dominio e o banco de dados
DbSet<T>: Representa uma coleção de entidades no contexto que podem ser consultadas no banco de dados

DTO (Data Annotations Object)
São conjuntos de atributos que podem ser aplicados a classes e seus membros para fornecer metadados sobre como esses 
recursos devem ser tratados pelo sistema;

-Realizar validações de entrada de dados 
-Influenciar o comportamento do modelo de dados


