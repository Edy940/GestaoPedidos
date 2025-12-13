variable "aws_region" {
  description = "Região da AWS"
  type        = string
  default     = "us-east-1"
}

variable "db_name" {
  description = "Nome do banco de dados"
  type        = string
  default     = "gestaopedidos"
}

variable "db_username" {
  description = "Usuário do PostgreSQL"
  type        = string
  default     = "gestao_user"
}

variable "db_password" {
  description = "Senha do PostgreSQL"
  type        = string
  sensitive   = true
}

variable "db_instance_class" {
  description = "Tipo da instância do RDS"
  type        = string
  default     = "db.t3.micro"
}
variable "product_images_bucket_name" {
  description = "Nome do bucket S3 para armazenar fotos dos produtos"
  type        = string
}

