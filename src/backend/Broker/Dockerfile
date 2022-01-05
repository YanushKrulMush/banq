FROM maven:3.8.4-openjdk-8-slim AS build
COPY src /usr/src/app/src
COPY pom.xml /usr/src/app
RUN mvn -f /usr/src/app/pom.xml clean package

FROM gcr.io/distroless/java
COPY --from=build /usr/src/app/target/*.jar /usr/app/api.jar
EXPOSE 8080
ENTRYPOINT ["java","-jar","/usr/app/api.jar"]