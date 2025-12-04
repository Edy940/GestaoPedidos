resource "aws_db_subnet_group" "rds_subnet_group" {
  name       = "gestaopedidos-subnet-group"
  subnet_ids = [
    aws_subnet.public_1.id,
    aws_subnet.public_2.id
  ]

  tags = {
    Name = "gestaopedidos-subnet-group"
  }
}
