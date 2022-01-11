minikube start
dapr init -k

# zipkin
kubectl create deployment zipkin --image openzipkin/zipkin
kubectl expose deployment zipkin --type ClusterIP --port 9411
kubectl apply -f ./dapr/env/tracing.yaml

# redis
helm repo add bitnami https://charts.bitnami.com/bitnami
helm install redis bitnami/redis
kubectl apply -f ./dapr/env/pubsub.yaml
kubectl apply -f ./dapr/env/statestore.yaml

# ingress/nginx
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm install nginx-ingress ingress-nginx/ingress-nginx -f ./dapr/ingress/dapr-annotations.yaml
kubectl create -f ./dapr/ingress/ingress-routes.yaml

# keycloak
kubectl create -f https://raw.githubusercontent.com/keycloak/keycloak-quickstarts/latest/kubernetes-examples/keycloak.yaml
minikube service --url keycloak

#postgres
kubectl create -f postgres-configmap.yaml 
kubectl create -f postgres-storage.yaml 
kubectl create -f postgres-deployment.yaml 
kubectl create -f postgres-service.yaml

# dotnet
minikube docker-env | Invoke-Expression
docker build ./src/backend/Internal -t dotnet-app:latest
kubectl apply -f ./dapr/dotnet.yaml

# totally java
minikube docker-env | Invoke-Expression
docker build ./src/backend/Broker -t java-app:latest
kubectl apply -f ./dapr/dotnet.yaml

# metrics
kubectl create namespace dapr-monitoring
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm repo update
helm install dapr-prom prometheus-community/prometheus -n dapr-monitoring
helm install dapr-prom prometheus-community/prometheus -n dapr-monitoring --set alertmanager.persistentVolume.enable=false --set pushgateway.persistentVolume.enabled=false --set server.persistentVolume.enabled=false

helm repo add grafana https://grafana.github.io/helm-charts
helm install grafana grafana/grafana -n dapr-monitoring --set persistence.enabled=false
kubectl get secret --namespace dapr-monitoring grafana -o jsonpath="{.data.admin-password}" | base64 --decode ; echo
kubectl port-forward svc/grafana 8081:80 -n dapr-monitoring
