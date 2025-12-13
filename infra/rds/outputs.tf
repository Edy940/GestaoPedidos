output "db_endpoint" {
  value = aws_db_instance.postgres.address
}

output "db_port" {
  value = aws_db_instance.postgres.port
}

output "db_name" {
  value = aws_db_instance.postgres.db_name
}

output "product_images_bucket_name" {
  description = "Nome do bucket S3 onde as fotos de produtos são armazenadas"
  value       = aws_s3_bucket.product_images.bucket
}

