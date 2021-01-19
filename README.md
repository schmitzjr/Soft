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
<li>Api2: http://localhost:33444</li>
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
<li>Api2: http://localhost:33444/index.html</li>
</ul>

<h1></h1>

<p><strong>Utilizando Docker.</strong></p>
<ul>
<li>Utilizar docker desktop</li>
<li>Alterar containers para Linux</li>
<li>Execultar em prompt comando:</li>
</ul>

<ul>
<li><strong>Rodar Dockerfile em Softplan.Apis\Softpla.Api1</strong></li>
<li><code>dotnet publish -c Release -o publish</code></li>
<li><code>docker build -t softplan-webapi1:v1 .</code></li>
<li><code>docker run -p 44555:80 softplan-webapi1:v1</code></li>
</ul>

<ul>
<li><strong>Rodar Dockerfile em Softplan.Apis\Softpla.Api2</strong></li>
<li><code>dotnet publish -c Release -o publish</code></li>
<li><code>docker build -t softplan-webapi2:v1 .</code></li>
<li><code>docker run -p 33444:80 softplan-webapi2:v1</code></li>
</ul>

<ul>
<li><strong>Acessar APIs em:</strong></li>
<li>Api1: http://localhost:44555</li>
<li>Api2: http://localhost:33444</li>
</ul>

<ul>
<li><strong>Acessar Swagger em:</strong></li>
<li>Api1: http://localhost:44555</li>
<li>Api2: http://localhost:33444</li>
</ul>

</div>          
