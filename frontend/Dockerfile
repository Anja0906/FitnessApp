FROM node:21

RUN npm install -g @angular/cli
WORKDIR /angular-app
COPY package*.json ./

RUN npm cache clean --force
RUN npm ci --legacy-peer-deps

COPY . .
EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]
