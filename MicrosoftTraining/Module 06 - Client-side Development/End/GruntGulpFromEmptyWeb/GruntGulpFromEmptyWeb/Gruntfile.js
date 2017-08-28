/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["Scripts/app/*", "temp/"],
        concat: {
            all: {
                src: ['TypeScript/Tastes.js', 'TypeScript/Food.js'],
                dest: 'temp/combined.js'
            }
        },
        jshint: {
            files: ['temp/*.js'],
            options: {
                '-W069': false
            }
        },
        uglify: {
            all: {
                src: ['temp/combined.js'],
                dest: 'Scripts/app/combined.min.js'
            }
        },
        watch: {
            files: ["TypeScript/**/*.js"], tasks: ["all"]
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask("all", ['clean', 'concat', 'jshint', 'uglify']);
};

