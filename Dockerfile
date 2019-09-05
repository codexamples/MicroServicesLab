FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

# -------------- Step 1. Copy only required files better caching
# 1.1 Copy solution 
COPY *.sln .
# 1.2 Copy csproj and create folders for projects
COPY ./*/*.csproj ./
# 1.3 Create folders for projects
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}/; done
# 1.4 Copy tests
COPY ./Tests/*/*.csproj ./
# 1.5 Create projects for tests
RUN for file in $(ls *.csproj); do mkdir -p Tests/${file%.*} && mv $file Tests/${file%.*}/; done

# 1.6
# LAB Q
RUN dotnet restore

# -------------- Step 2. Copy all other files
COPY . .


# -------------- Step 3. Build all
RUN dotnet build -c Debug

# TODO Support flag test=false
# TODO Export test xml results  files
# -------------- Step 4. Run tests
RUN dotnet vstest Tests/*/bin/Debug/*/*Tests.dll

#-------------- Step 5. Build runtime image
RUN dotnet publish Service.App -c Debug -o /app/out --no-restore

FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Service.App.dll"]
