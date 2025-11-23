📘 Global AuraMonitor – Sistema de Monitoramento de Humor Organizacional

O Global AuraMonitor é uma plataforma destinada ao acompanhamento do bem-estar emocional e engajamento de colaboradores dentro de uma organização.
O sistema permite registrar check-ins de humor, gerar insights e exibir uma visão consolidada do clima organizacional através de painéis e relatórios.

A arquitetura segue princípios de Clean Architecture / DDD, garantindo separação clara de responsabilidades, alta manutenção e escalabilidade.

🎯 Objetivos do Projeto

Permitir que colaboradores realizem check-ins de humor de forma simples.

Disponibilizar para gestores uma visão consolidada do clima da equipe.

Registrar, processar e exibir informações de forma organizada e segura.

Manter uma arquitetura limpa com camadas bem definidas:
Domain → Application → Infrastructure → Web → Dashboard.

🏛️ Arquitetura da Solução

A solução está dividida em múltiplos projetos:

Global-AuraMonitor/
 ├── AuraMonitor.Domain
 ├── AuraMonitor.Application
 ├── AuraMonitor.Infrastructure
 ├── AuraMonitor.Web
 └── AuraMonitor.Dashboard

🔹 1. Domain (Núcleo da Aplicação)

Camada que contém:

Entidades do sistema

Regras de negócio

Contratos essenciais (interfaces base)

Aqui ficam os conceitos fundamentais do domínio, como:

Colaborador

Gestor

Check-in de humor

Indicadores de clima

🔹 2. Application (Casos de Uso / Orquestração)

Contém a lógica que coordena as operações do sistema:

Services responsáveis por cada funcionalidade

DTOs / ViewModels para transporte entre camadas

Validações de entrada e regras de fluxo

Essa camada não conhece detalhes de banco ou UI — apenas fluxo de operações.

🔹 3. Infrastructure (Persistência e Serviços Externos)

Implementa:

Acesso a banco de dados (via Entity Framework)

Repositórios concretos

Contexto da aplicação

Migrations

Configurações de persistência

Aqui ficam todas as integrações do mundo externo.

🔹 4. Web (API ou Web MVC)

Camada responsável por expor funcionalidades ao usuário ou a outras aplicações.

Inclui:

Controllers

Rotas

Endpoints REST (se API)

Views e validações (se MVC)

🔹 5. Dashboard (Visualização e Analytics)

Camada opcional destinada à visualização de métricas:

Gráficos

Indicadores

Painéis executivos

Permite acompanhar o clima organizacional em tempo real.

🗃️ Banco de Dados

O projeto utiliza Entity Framework Core, permitindo:

Migrations versionadas

Mapeamento objeto-relacional (ORM)

Repositórios baseados em interfaces do domínio

🚀 Como Executar o Projeto
✔️ Pré-requisitos

.NET 8 ou superior instalado

SQL Server / PostgreSQL (dependendo da configuração)

Git instalado

✔️ Passo a passo

Clone o repositório:

git clone https://github.com/FabricioJ0se/Global-AuraMonitor


Acesse o diretório:

cd Global-AuraMonitor


Aplique as migrations:

dotnet ef database update -p AuraMonitor.Infrastructure -s AuraMonitor.Web


Execute o projeto:

dotnet run --project AuraMonitor.Web


Acesse no navegador:

http://localhost:5018/swagger

🧪 Funcionalidades Implementadas

Registro e login de colaboradores

Registro de humor (check-in diário)

Consulta de histórico

Painel com estatísticas de humor

Separação clara de responsabilidades entre camadas

Repositórios e serviços de aplicação

Interface web funcional

📈 Possíveis Melhorias Futuras

Implementar filtros avançados e pesquisa paginada

Adicionar autenticação JWT (se API)

Criar value objects no domínio

Ampliar o dashboard com gráficos mais avançados

Incluir testes unitários e integração

📄 Licença

Projeto acadêmico — uso livre para fins educacionais.


