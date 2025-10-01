# Gestão de Mensalidades - Frontend Blazor

Frontend em Blazor Server para o sistema de gestão de assinaturas e mensalidades.

## 🚀 Tecnologias Utilizadas

- **Blazor Server** - Framework web interativo
- **Bootstrap 5** - Framework CSS
- **Font Awesome** - Ícones
- **ASP.NET Core** - Backend
- **HttpClient** - Comunicação com API
- **Authentication** - Autenticação com cookies

## 📋 Funcionalidades Implementadas

### ✅ Autenticação
- Login com JWT
- Logout automático
- Proteção de rotas
- Gerenciamento de sessão

### ✅ Gestão de Clientes
- Listagem de clientes
- Criação de novos clientes
- Edição de dados
- Remoção de clientes
- Validação de formulários

### ✅ Interface Moderna
- Design responsivo
- Componentes interativos
- Notificações toast
- Modais para formulários
- Dashboard com cards

## 🛠️ Como Executar

### Pré-requisitos
- .NET 9 SDK
- API rodando em `https://localhost:7001`

### 1. Restaurar dependências
```bash
dotnet restore
```

### 2. Executar a aplicação
```bash
dotnet run
```

### 3. Acessar a aplicação
- **URL**: `https://localhost:5001` ou `http://localhost:5000`
- **Login**: `admin@gestaomensalidades.com`
- **Senha**: `admin123`

## 📁 Estrutura do Projeto

```
GestaoMensalidades.Web/
├── Components/           # Componentes Blazor
│   ├── Layout/          # Layouts da aplicação
│   └── _Imports.razor   # Imports globais
├── Models/              # Modelos de dados
├── Pages/               # Páginas da aplicação
├── Services/            # Serviços de negócio
├── wwwroot/             # Arquivos estáticos
└── Program.cs           # Configuração da aplicação
```

## 🔧 Configuração

### API Settings
Edite o arquivo `appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001"
  }
}
```

### Autenticação
A aplicação usa autenticação baseada em cookies que se comunica com a API via JWT.

## 📱 Páginas Disponíveis

### `/login`
- Página de login
- Validação de formulário
- Redirecionamento automático

### `/`
- Dashboard principal
- Cards com estatísticas
- Menu de navegação

### `/customers`
- Listagem de clientes
- CRUD completo
- Modais para edição

## 🎨 Componentes

### ApiService
- Comunicação com a API
- Gerenciamento de tokens
- Tratamento de erros

### AuthService
- Autenticação de usuários
- Gerenciamento de sessão
- Claims do usuário

### Layout
- Sidebar responsiva
- Menu de navegação
- Header com usuário

## 🔒 Segurança

- Autenticação obrigatória
- Proteção de rotas
- Validação de formulários
- Sanitização de dados

## 📊 Funcionalidades Futuras

- [ ] Gestão de Assinaturas
- [ ] Controle de Pagamentos
- [ ] Relatórios e Dashboards
- [ ] Notificações
- [ ] Exportação de dados

## 🚀 Deploy

### Desenvolvimento
```bash
dotnet run --environment Development
```

### Produção
```bash
dotnet publish --configuration Release
```

## 📞 Suporte

Para dúvidas ou problemas:
- Verifique se a API está rodando
- Confirme as configurações de conexão
- Verifique os logs da aplicação

