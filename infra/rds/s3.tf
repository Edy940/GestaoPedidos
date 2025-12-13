resource "aws_s3_bucket" "product_images" {
  bucket = var.product_images_bucket_name

  tags = {
    Name        = "gestaopedidos-product-images"
    Environment = "dev"
    Project     = "GestaoPedidos"
  }
}

# Bloquear acesso público (boa prática)
resource "aws_s3_bucket_public_access_block" "product_images" {
  bucket = aws_s3_bucket.product_images.id

  block_public_acls       = true
  block_public_policy     = true
  ignore_public_acls      = true
  restrict_public_buckets = true
}

# Criptografia padrão no bucket
resource "aws_s3_bucket_server_side_encryption_configuration" "product_images" {
  bucket = aws_s3_bucket.product_images.id

  rule {
    apply_server_side_encryption_by_default {
      sse_algorithm = "AES256"
    }
  }
}

# Versionamento (opcional mas bom p/ portfólio)
resource "aws_s3_bucket_versioning" "product_images" {
  bucket = aws_s3_bucket.product_images.id

  versioning_configuration {
    status = "Enabled"
  }
}
