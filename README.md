<div class="Box-body">

<article class="markdown-body entry-content p-5" itemprop="text"><p><strong>SoftPlan</strong></p>

<h1></h1>
          
<p><strong>Desafio técnico</strong></p>
<p><strong>Desenvolvimento de APIs para cálculo de juros.</strong></p>
<ul>
<li>APIs desenvolvidas utilizando .Net Core 3.1.</li>
<li>IDE utilizada VSCode.</li>
</ul>

<h1></h1>

<p><strong>Intruções para inicialização das aplicações em modo debug.</strong></p>
<ul>
<li>Selecionar / Utilizar a opção compounds (Api1/Api2) da launch.json, para rodar as duas APIs simultâneamente.</li>
<li>Comando dotnet run.</li>
</ul>

<h1></h1>

<p><strong>Endereço das aplicações.</strong></p>
<ul>
<li>Api1: http://localhost:44555</li>
<li>Api2: http://localhost:44333</li>
</ul>

<h1></h1>

<p><strong>Autenticação.</strong></p>
<ul>       
<li>/api/Authenticate</li>
<li>Authorization é feita utilizando Bearer token jwt no header.</li>  
<li>user: admin</li>
<li>password: admin</li>         
</ul>

<h1></h1>

<p><strong>Consulte Swagger.</strong></p>
<ul>
<li>Api1: http://localhost:44555/index.html</li>
<li>Api2: http://localhost:44333/index.html</li>
</ul>

<h1></h1>

<p><strong>Utilizando Docker.</strong></p>
<ul>
<li>Utilizar docker desktop</li>
<li>Alterar containers para Linux</li>
<li>Execultar em prompt comando:</li>
</ul>

<ul>
<li>Nas pastas Softplan.Api2 alterar no arquivo <code>appsettings.json</code> a variável <code>RunInDocker: true</code></li>
<li>Nas pastas Softplan.Api1 e Softplan.Api2 rodar o comando:</li>
<li><code>dotnet publish -c Release -o publish</code></li>
<li>Na raiz do projeto rodar o comando:</li>
<li><code>docker network create -d bridge softplan-network</code></li>         
<li>Na raiz do projeto rodar o arquivo docker-compose.yml com o comando:</li>
<li><code>docker-compose up -d --build</code></li>
</ul>

<ul>
<li><strong>Acessar APIs em:</strong></li>
<li>Api1: http://localhost:44555</li>
<li>Api2: http://localhost:44333</li>
</ul>

<ul>
<li><strong>Acessar Swagger em:</strong></li>
<li>Api1: http://localhost:44555</li>
<li>Api2: http://localhost:44333</li>
</ul>

</div>          
