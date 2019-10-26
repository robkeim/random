import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText


def read_password():
    with open("password.txt") as file:
        return file.read()


def send(receiver, subject, body):
    sender = 'robkeim@gmail.com'

    message = MIMEMultipart()
    message['From'] = sender
    message['To'] = receiver
    message['Subject'] = "[ClassPass] " + subject
    message.attach(MIMEText(body, 'plain'))

    session = smtplib.SMTP('smtp.gmail.com', 587)
    session.starttls()
    session.login(sender, read_password())
    text = message.as_string()
    session.sendmail(sender, receiver, text)
    session.quit()