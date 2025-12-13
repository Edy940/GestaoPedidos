resource "aws_db_instance" "postgres" {
  identifier        = "gestaopedidos-postgres"
  engine            = "postgres"
 # engine_version removido para deixar a AWS escolher automaticamente
  instance_class    = var.db_instance_class
  allocated_storage = 20

  db_name  = var.db_name
  username = var.db_username
  password = var.db_password

  publicly_accessible = true
  skip_final_snapshot = true

  vpc_security_group_ids = [aws_security_group.rds_sg.id]
  db_subnet_group_name  = aws_db_subnet_group.rds_subnet_group.name

  tags = {
    Name = "gestaopedidos-postgres"
  }
}
