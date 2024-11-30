CREATE DATABASE bd_sports
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
	CREATE SCHEMA if not exists sys
    AUTHORIZATION postgres;
	

	create table if not exists sys.usuario_tb
	(
		id_usuario serial not null primary key,
		login varchar not null,
		normalizedLogin varchar,
		senha varchar not null,		
		email varchar(300)		,
		tipo_usuario integer default 0
	);	
	


	create table if not exists sys.usuario_claim_tb(
		id_usuario bigint not null,
		claim varchar not null,
		constraint id_usuario_fk foreign key(id_usuario)
		references sys.usuario_tb(id_usuario)
	);
	
	
	create table if not exists sys.club_tb(
		id_club serial not null primary key,
		id_usuario bigint null,
		nome_fantasia varchar,
		razao_social varchar,
		cpf_cnpj bigint,
		is_pj boolean,
		endereco varchar,
		numero integer,
		cidade varchar,
		cep varchar(20),
		uf varchar(2),
		imagem_base64 varchar,
		is_verificado boolean,
		data_fundacao timestamp null,
		nome_presidente varchar null,
		sobre_o_clube varchar,
		celular varchar
	);		



	create table if not exists sys.aluno_tb(
		id_aluno serial not null primary key,
		id_usuario bigint,
		id_club bigint null,
		nome varchar not null,
		documento varchar,
		cpf_cnpj bigint,
		foto_base64 varchar,
		data_nascimento timestamp,
		email varchar(300),
		telefone varchar(50),
		celular varchar(50),
		is_pcd BOOLEAN,
		descricao_pcd varchar, 
		posicao_jogador varchar(50),		
		endereco varchar,
		numero integer,
		cidade varchar,
		uf varchar,
		cep varchar,
		complemento varchar(200),
		camiseta_numero integer,
		is_verificado boolean null,
		gols_marcados integer null,
		CONSTRAINT id_usuario_fk foreign key (id_usuario)
		references sys.usuario_tb(id_usuario)	,
		CONSTRAINT id_club_fk foreign key (id_club)
		references sys.club_tb(id_club)	
	);
	
	create table if not exists sys.administrador_tb(
		id_administrador serial not null primary key,
		id_usuario bigint null,
		nome varchar not null,
		documento varchar,
		cpf_cnpj bigint,
		foto_base64 varchar,
		data_nascimento timestamp,
		email varchar(300),
		telefone varchar(50),
		celular varchar(50),
		endereco varchar,
		numero integer,
		cidade varchar,
		uf varchar,
		cep varchar,
		complemento varchar(200),
		CONSTRAINT id_usuario_adm_fk foreign key (id_usuario)
		references sys.usuario_tb(id_usuario)
	);
	

	create table if not exists sys.responsavel_aluno_tb(
		id_responsavel serial not null primary key,
		id_aluno bigint not null,
		nome_responsavel varchar(300),
		documento_responsavel varchar,
		cpf_responsavel bigint,
		grau_parentesco varchar,
		Constraint id_aluno_fk foreign key (id_aluno)
		references sys.aluno_tb(id_aluno)
	);
	
	create table if not exists sys.solicitacao_aluno_club_tb(
		id_solicitacao serial not null primary key,
		id_aluno bigint not null,
		id_club bigint not null,
		data_solicitacao timestamp not null,
		is_aceito boolean null,
		is_visto boolean null,
		constraint id_aluno_fk foreign key(id_aluno)
		references sys.aluno_tb(id_aluno),
		constraint id_club_gk foreign key(id_club)
		references sys.club_tb(id_club)
	);
	
	create table if not exists sys.notificacao_tb(
		id_notificacao serial not null primary key,
		id_aluno bigint  null,
		id_club bigint null,
		id_administrador bigint null,
		data_notificacao timestamp not null,
		is_visto boolean null,
		tipo_usuario integer not null,
		notificacao varchar not null,	
		imagem_notificacao varchar null,
		link_redirect varchar null,
		constraint id_aluno_fk foreign key (id_aluno)
		references sys.aluno_tb(id_aluno),
		constraint id_club_fk foreign key (id_club)
		references sys.club_tb(id_club)
	);
	

	
	create table if not exists sys.aluno_club_tb(		
		id_club bigint not null,
		id_aluno bigint not null,
		data_ingresso timestamp null,
		gols_marcados integer null
	);
	
	
	create table if not exists sys.agenda_evento_tb(
		id_evento serial not null primary key,
		data_hora timestamp not null,
		tipo_evento integer not null,
		id_club1 bigint not null,
		id_club2 bigint null,
		endereco_resumido varchar,
		imagem_base64 varchar,
		observacoes varchar(300),
		gols_club1 integer null,
		gols_club2 integer null,
		is_encerrado boolean null,
		hora_evento varchar(10) null,
		constraint id_clube1_fk foreign key(id_club1)
		references sys.club_tb(id_club),
		constraint id_clube2_fk foreign key(id_club2)
		references sys.club_tb(id_club)
	);
	

	
		create table if not exists sys.patrocinador_tb(
		id_patrocinador serial not null primary key,
		nome_razaosocial varchar not null,
		email varchar not null,
		is_pj boolean not null,
		cpf_cnpj bigint null,
		telefone varchar null,
		celular varchar not null,
		mensagem varchar null,
		logotipo_base64 varchar null,
	 	status integer not null,
		observacoes varchar null,
		instagram_url varchar null,
		linkedin_url varchar null,
		site_url varchar null,
		ordem_apresentacao integer null		
	);
	
	
	
	create table if not exists sys.atleta_evento_tb(
		id_lancamento serial not null primary key,
		id_evento integer not null,
		id_aluno integer not null,
		id_clube integer not null,
		gols_marcados integer not null,
		constraint id_aluno_fk foreign key(id_aluno)
		references sys.aluno_tb(id_aluno),
		constraint id_clube_fk foreign key (id_clube)
		references sys.club_tb(id_club)	
	);
	
	
--Tornar usuario administrador
--insert into sys.usuario_claim_tb (id_usuario, claim) values (1, 'adm');


--Criar Role usuario Club
--insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'club');
--insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'upd-club');
--insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'del-club');
--insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'read-club');
	

	--ALTER TABLES
	--alter table if exists sys.aluno_tb add column if not exists gols_marcados integer null;
	--alter table sys.agenda_evento_tb add column if not exists hora_evento varchar(10) null;
	
	--drop table sys.atleta_evento_tb
	


--select  atl.id_aluno, atl.nome, cl.nome_fantasia as nome_clube, atl.foto_base64, sum(aevt.gols_marcados) as gols_marcados from sys.agenda_evento_tb as evt
--inner join sys.atleta_evento_tb as  aevt ON evt.id_evento = aevt.id_evento
--inner join sys.aluno_tb as atl ON atl.id_aluno = aevt.id_aluno
--inner join sys.club_tb as cl on cl.id_club = atl.id_club
--where evt.data_hora between to_timestamp('01-01-2023','dd-MM-yyyy') AND to_timestamp('01-01-2025','dd-MM-yyyy')
--group by atl.id_aluno, atl.nome, cl.nome_fantasia 
--order by gols_marcados desc
--limit 3


