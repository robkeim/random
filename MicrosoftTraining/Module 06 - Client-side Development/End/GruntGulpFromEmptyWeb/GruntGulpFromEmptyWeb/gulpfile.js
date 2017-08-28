/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var clean = require('gulp-clean');
var concat = require('gulp-concat');
var jshint = require('gulp-jshint');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');

gulp.task('default', function () {
    gulp.src('Scripts/app/*').pipe(clean());
    gulp.src('Scripts/app/*').pipe(clean());
    gulp.src(['TypeScript/Tastes.js', 'TypeScript/Food.js'])
            .pipe(concat("combined.js"))
            .pipe(jshint())
            .pipe(uglify())
            .pipe(rename({
                extname: '.min.js'
            }))
            .pipe(gulp.dest('Scripts/app'))
});

gulp.task("watcher", function () {
    gulp.watch("TypeScript/**/*.js", ['all']);
});