FROM node:13-alpine AS builder

RUN npm install -g @angular/cli
WORKDIR /app/
ADD /package.json /app/package.json
RUN npm install
ADD . /app
RUN ng build

FROM nginx:alpine
COPY --from=builder /app/dist/app /usr/share/nginx/html