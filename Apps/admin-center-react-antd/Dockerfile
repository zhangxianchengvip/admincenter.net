# 使用官方 Nginx 镜像作为父镜像
FROM nginx

# 复制你的静态文件到容器的 /usr/share/nginx/html 目录
COPY dist/ /usr/share/nginx/html

# 复制 nginx 默认配置文件
COPY nginx.conf /etc/nginx/conf.d/default.conf