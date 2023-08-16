CREATE DATABASE bd_sports
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
	CREATE SCHEMA sys
    AUTHORIZATION postgres;
	
	select * from sys.usuario_tb
	
	
	create table sys.usuario_tb
	(
		id_usuario serial not null primary key,
		login varchar not null,
		senha varchar not null,
		tipo integer not null,
		email varchar(300)		
	);	
	
	create table sys.aluno_tb(
		id_aluno serial not null primary key,
		id_usuario bigint,
		nome varchar not null,
		documento varchar,
		cpf_cnpj bigint,
		data_nascimento timestamp,
		email varchar(300),
		telefone varchar(50),
		celular varchar(50),
		is_pcd bit,
		descricao_pcd varchar, 
		posicao_jogador varchar(50),		
		endereco varchar,
		numero integer,
		cidade varchar,
		uf varchar,
		cep varchar,
		CONSTRAINT id_usuario_fk foreign key (id_usuario)
		references sys.usuario_tb(id_usuario)	
	);
	
	create table responsavel_aluno_tb(
		id_responsavel serial not null primary key,
		id_aluno bigint not null,
		nome_responsavel varchar(300),
		documento_responsavel varchar,
		cpf_responsavel bigint,
		grau_parentesco varchar,
		Constraint id_aluno_fk foreign key (id_aluno)
		references sys.aluno_tb(id_aluno)
	);
	
	