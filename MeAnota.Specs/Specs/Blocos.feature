Funcionalidade: Listar, criar e apagar blocos de notas
	Como um usuário cadastrado e logado no sistema
	Eu quero poder listar, criar e apagar blocos de notas

Contexto: Estar cadastrado e logado no site
	Dado que existe um usuário "eu@templecoding.com"
	E que o usuário "eu@templecoding.com" está logado no site

Cenario: Listar os blocos cadastrados
	Dado que eu tenho os seguintes blocos cadastrados
		| Nome            | Email               |
		| Bloco Principal | eu@templecoding.com |
		| Bloco numero 2  | eu@templecoding.com |
	Quando eu visito "/"
	Então eu vejo
		| Nome            |
		| Bloco Principal |
		| Bloco numero 2  |

Cenário: Listar os blocos cadastrados somente do usuario logado
	Dado que existe um usuário "outro@email.com"
	E que eu tenho os seguintes blocos cadastrados
		| Nome            | Email               |
		| Bloco principal | eu@templecoding.com |
		| Bloco numero 2  | outro@vintem.com.br |
	Quando eu visito "/"
	Entao eu vejo
		| Nome            |
		| Bloco principal |
	Mas eu não vejo
		| Field | Value          |
		| Nome  | Bloco numero 2 |

Cenário: Adicionar um novo bloco com sucesso
	Quando eu visito "/"
		E eu clico em "Criar Novo"
		E eu preencho
			| Field | Value      |
			| Nome  | Spec Bloco |
		E eu clico no botão "Criar"
	Entao eu vejo "Spec Bloco"
		E eu vejo "Bloco adicionado com sucesso"

Cenário: Excluir um bloco com sucesso
	Dado que eu tenho os seguintes blocos cadastrados
		| Nome            | Email               |
		| Bloco principal | eu@templecoding.com |
	Quando eu visito "/"
		E eu clico em "Excluir"
		E eu confirmo a mensagem de cancelamento "Deseja realmente excluir esse Bloco?"
	Então eu vejo "Bloco excluído com sucesso"
	Mas eu não vejo "Bloco principal"